using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SQLite4Unity3d;

public class RepasoGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text[] textosIdiomas;   // Un TMP_Text por idioma en el orden de ConfigurarIdiomas
    [SerializeField] private TMP_Text feedbackText;      // Opcional: para mensajes de error
    
    // Banderas en el mismo orden que textosIdiomas
    [SerializeField] private Image[] flagImages;         

    private string[] languageCodes;    // Orden de idiomas (es,en,it,fr,de,nl)
    private int minGroupId = 1;
    private int maxGroupId = 80;

    // Lista completa de grupos disponibles en la categoría
    private List<int> todosLosGrupos = new List<int>();
    // Lista de grupos pendientes en este ciclo (sin repetir)
    private List<int> gruposPendientes = new List<int>();

    // Claves PlayerPrefs para el rango de categoría
    private const string CATEGORY_MIN_KEY = "category_min_group";
    private const string CATEGORY_MAX_KEY = "category_max_group";

    private void Start()
    {
        CargarOrdenIdiomas();
        AsignarBanderas();

        CargarCategoriaActual();
        CargarListaDeGrupos();  // rellenamos todosLosGrupos y gruposPendientes
        MostrarSiguienteGrupo();
    }

    private void CargarOrdenIdiomas()
    {
        string order = PlayerPrefs.GetString("language_order", "es,en,it,fr,de,nl");
        languageCodes = order.Split(',');

        if (languageCodes.Length == 0)
        {
            Debug.LogError("language_order vacío. Usando 'es' por defecto.");
            languageCodes = new string[] { "es" };
        }

        Debug.Log("Orden de idiomas (RepasoGame): " + string.Join(",", languageCodes));
    }

    /// <summary>
    /// Asigna a cada Image de flagImages la bandera que corresponde
    /// al idioma en esa posición según language_order.
    /// Requiere que las sprites estén en Resources/Flags/es.png, en.png, etc.
    /// </summary>
    private void AsignarBanderas()
    {
        if (flagImages == null || flagImages.Length == 0)
        {
            Debug.LogWarning("RepasoGame: flagImages no asignado en el Inspector.");
            return;
        }

        if (languageCodes == null || languageCodes.Length == 0)
        {
            Debug.LogWarning("RepasoGame: languageCodes vacío al intentar AsignarBanderas.");
            return;
        }

        int count = Mathf.Min(flagImages.Length, languageCodes.Length);

        for (int i = 0; i < count; i++)
        {
            string code = languageCodes[i]; // es, en, fr, de, it, nl...
            // Asumimos que las sprites se llaman exactamente "es", "en", "fr"... en Assets/Resources/Flags/
            Sprite s = Resources.Load<Sprite>($"Flags/{code}");

            if (s != null)
            {
                flagImages[i].sprite = s;
                flagImages[i].gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"RepasoGame: No encontré la bandera Resources/Flags/{code}.png");
                flagImages[i].gameObject.SetActive(false); // si no hay sprite, ocultamos la imagen
            }
        }

        // Si hay más slots de banderas que idiomas activos, los ocultamos
        for (int i = count; i < flagImages.Length; i++)
        {
            flagImages[i].gameObject.SetActive(false);
        }
    }

    private void CargarCategoriaActual()
    {
        minGroupId = PlayerPrefs.GetInt(CATEGORY_MIN_KEY, 1);
        maxGroupId = PlayerPrefs.GetInt(CATEGORY_MAX_KEY, 80);
        Debug.Log($"RepasoGame - Rango de grupos: {minGroupId} a {maxGroupId}");
    }

    /// <summary>
    /// Carga todos los group_id de la categoría que tengan al menos una palabra
    /// en alguno de los idiomas configurados. Se guardan en todosLosGrupos
    /// y se inicializa gruposPendientes barajado.
    /// </summary>
    private void CargarListaDeGrupos()
    {
        todosLosGrupos.Clear();
        gruposPendientes.Clear();

        if (languageCodes == null || languageCodes.Length == 0)
        {
            Debug.LogError("RepasoGame: languageCodes vacío al cargar lista de grupos.");
            return;
        }

        // Usamos códigos en minúsculas y parámetros, como en el resto de juegos
        var lowerCodes = languageCodes
            .Where(c => !string.IsNullOrEmpty(c))
            .Select(c => c.ToLowerInvariant())
            .Distinct()
            .ToList();

        if (lowerCodes.Count == 0)
        {
            Debug.LogError("RepasoGame: no hay códigos de idioma válidos.");
            return;
        }

        string placeholders = string.Join(",", lowerCodes.Select(_ => "?"));

        string sql = @"
            SELECT DISTINCT W.group_id AS GroupId
            FROM Words W
            JOIN Languages L ON L.id = W.language_id
            WHERE W.group_id BETWEEN ? AND ?
              AND lower(L.code) IN (" + placeholders + @")
            ORDER BY W.group_id;
        ";

        // Parámetros: min, max, luego todos los códigos
        var parametros = new List<object> { minGroupId, maxGroupId };
        parametros.AddRange(lowerCodes);

        var rows = DataBaseManager.DB.Query<GroupIdRow>(sql, parametros.ToArray());
        foreach (var row in rows)
        {
            todosLosGrupos.Add(row.GroupId);
        }

        if (todosLosGrupos.Count == 0)
        {
            Debug.LogWarning("RepasoGame: No se encontraron grupos en esta categoría para los idiomas seleccionados.");
            if (feedbackText != null)
                feedbackText.text = "No hay palabras en esta categoría.";
            return;
        }

        // Inicializamos los pendientes con todos los grupos y los barajamos
        gruposPendientes = new List<int>(todosLosGrupos);
        BarajarLista(gruposPendientes);

        Debug.Log($"RepasoGame: Cargados {todosLosGrupos.Count} grupos. Empezamos ciclo nuevo.");
    }

    /// <summary>
    /// Método público que llamas desde el botón "Siguiente".
    /// </summary>
    public void OnClickSiguiente()
    {
        MostrarSiguienteGrupo();
    }

    private void MostrarSiguienteGrupo()
    {
        if (textosIdiomas == null || textosIdiomas.Length == 0)
        {
            Debug.LogError("RepasoGame: Asigna al menos un TMP_Text en 'textosIdiomas' en el Inspector.");
            return;
        }

        if (todosLosGrupos.Count == 0)
        {
            // No hay grupos cargados (por ejemplo, categoría vacía)
            if (feedbackText != null)
                feedbackText.text = "No hay palabras en esta categoría.";
            return;
        }

        // Si ya hemos usado todos los grupos en este ciclo, empezamos uno nuevo barajado
        if (gruposPendientes.Count == 0)
        {
            gruposPendientes = new List<int>(todosLosGrupos);
            BarajarLista(gruposPendientes);
            Debug.Log("RepasoGame: Se han mostrado todos los grupos. Comenzamos nueva vuelta.");
        }

        int groupId = gruposPendientes[0];
        gruposPendientes.RemoveAt(0);  // lo sacamos de los pendientes

        Dictionary<string, string> textosPorIdioma = CargarTextosDeGrupo(groupId);

        // Mostrar palabras según el orden de language_order y la cantidad de textos que tengas en la UI
        int count = Mathf.Min(languageCodes.Length, textosIdiomas.Length);
        for (int i = 0; i < count; i++)
        {
            string code = languageCodes[i]; // MISMO orden que ConfigurarIdiomas
            if (textosPorIdioma.TryGetValue(code, out string palabra))
            {
                textosIdiomas[i].text = palabra;
            }
            else
            {
                textosIdiomas[i].text = "---";
            }
        }

        // Si sobran TMP_Text (por ejemplo hay 6 textos y menos idiomas activos), los vaciamos
        for (int i = count; i < textosIdiomas.Length; i++)
        {
            textosIdiomas[i].text = "";
        }

        if (feedbackText != null)
            feedbackText.text = ""; // limpiar mensajes
    }

    private Dictionary<string, string> CargarTextosDeGrupo(int groupId)
    {
        var dict = new Dictionary<string, string>();

        string sql = @"
            SELECT L.code AS Code, W.text AS Text
            FROM Words W
            JOIN Languages L ON L.id = W.language_id
            WHERE W.group_id = ?;
        ";

        var rows = DataBaseManager.DB.Query<CodeTextRow>(sql, groupId);
        foreach (var row in rows)
        {
            dict[row.Code] = row.Text;
        }

        return dict;
    }

    /// <summary>
    /// Baraja una lista in-place usando Fisher-Yates.
    /// </summary>
    private void BarajarLista(List<int> lista)
    {
        for (int i = 0; i < lista.Count - 1; i++)
        {
            int j = UnityEngine.Random.Range(i, lista.Count);
            int tmp = lista[i];
            lista[i] = lista[j];
            lista[j] = tmp;
        }
    }

    // ====== Clases auxiliares para mapear resultados de SQLite4Unity3d ======

    private class GroupIdRow
    {
        public int GroupId { get; set; }   // mapea a W.group_id AS GroupId
    }

    private class CodeTextRow
    {
        public string Code { get; set; }   // L.code AS Code
        public string Text { get; set; }   // W.text AS Text
    }
}
