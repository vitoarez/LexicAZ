using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;

public class VerdadGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text palabraCentralText;   // Palabra en idioma principal
    [SerializeField] private Button[] botonesIdiomas;       // 4 botones para las traducciones

    [Header("Rachas")]
    [SerializeField] private TMP_Text rachaNumeroText;        // Número de la racha actual
    [SerializeField] private TMP_Text mejorRachaNumeroText;   // Mejor racha histórica

    private string[] languageCodes;   // Códigos de idiomas en orden (es,en,it,fr,de,nl)
    private int indiceVerdadero;      // Índice del botón que contiene la VERDAD (traducción correcta)

    private int rachaActual = 0;      // Contador de aciertos seguidos
    private int mejorRacha = 0;       // Mejor racha (guardada en PlayerPrefs)

    // Rango de grupos para la categoría seleccionada
    private int minGroupId = 1;
    private int maxGroupId = 80;

    // Recordar el último grupo para no repetir la misma palabra central seguida
    private int ultimoGrupoId = -1;

    // Claves para PlayerPrefs
    private const string PREF_MEJOR_RACHA = "VerdadGame_BestStreak";
    private const string CATEGORY_MIN_KEY = "category_min_group";
    private const string CATEGORY_MAX_KEY = "category_max_group";

    private void Start()
    {
        CargarOrdenIdiomas();

        // Leer rango de grupos desde PlayerPrefs (por defecto, todos los grupos 1–80)
        minGroupId = PlayerPrefs.GetInt(CATEGORY_MIN_KEY, 1);
        maxGroupId = PlayerPrefs.GetInt(CATEGORY_MAX_KEY, 80);
        Debug.Log($"VerdadGame — Rango de grupos: {minGroupId} a {maxGroupId}");

        CargarMejorRacha();
        ActualizarUIRachas();

        GenerarRonda();
    }

    private void CargarOrdenIdiomas()
    {
        string order = PlayerPrefs.GetString("language_order", "es,en,it,fr,de,nl");
        languageCodes = order.Split(',');

        if (languageCodes.Length < 5)
        {
            Debug.LogWarning(" Se necesitan al menos 5 idiomas en language_order para VerdadGame.");
        }
    }

    private void CargarMejorRacha()
    {
        mejorRacha = PlayerPrefs.GetInt(PREF_MEJOR_RACHA, 0);
        Debug.Log("Mejor racha (VerdadGame) cargada: " + mejorRacha);
    }

    private void GuardarMejorRacha()
    {
        PlayerPrefs.SetInt(PREF_MEJOR_RACHA, mejorRacha);
        PlayerPrefs.Save();
        Debug.Log("Mejor racha (VerdadGame) guardada: " + mejorRacha);
    }

    private void ActualizarUIRachas()
    {
        if (rachaNumeroText != null)
            rachaNumeroText.text = rachaActual.ToString();

        if (mejorRachaNumeroText != null)
            mejorRachaNumeroText.text = mejorRacha.ToString();
    }

    // ==========================================================
    //                     GENERAR RONDA
    // ==========================================================

    private void GenerarRonda()
    {
        try
        {
            if (languageCodes == null || languageCodes.Length < 5)
            {
                Debug.LogError("No hay suficientes idiomas configurados para generar una ronda.");
                if (palabraCentralText != null)
                    palabraCentralText.text = "Error idiomas";
                return;
            }

            if (botonesIdiomas == null || botonesIdiomas.Length < 4)
            {
                Debug.LogError("Debes asignar 4 botones en el array botonesIdiomas en el Inspector.");
                if (palabraCentralText != null)
                    palabraCentralText.text = "Error botones";
                return;
            }

            string mainLangCode = languageCodes[0];                             // Idioma principal
            string[] otherLangCodes = languageCodes.Skip(1).Take(4).ToArray();  // Otros 4 idiomas

            // 1) Obtener IDs de idiomas (case-insensitive)
            var codigos = new List<string> { mainLangCode };
            codigos.AddRange(otherLangCodes);
            Dictionary<string, int> langIds = ObtenerLanguageIds(codigos);

            // 2) Escoger un grupo que tenga palabras en los 5 idiomas, sin repetir el anterior si es posible
            int groupId = ObtenerGrupoConTodosLosIdiomasSinRepetir(langIds.Values.ToList(), ultimoGrupoId);
            if (groupId == -1)
            {
                Debug.LogError($"No se encontró ningún grupo con palabras en todos los idiomas necesarios entre {minGroupId} y {maxGroupId}.");
                if (palabraCentralText != null)
                    palabraCentralText.text = "Sin grupos";
                return;
            }

            // Guardamos el grupo actual para evitar repetir
            ultimoGrupoId = groupId;

            // 3) Palabra central (idioma principal)
            int mainLangId = langIds[mainLangCode];
            string palabraCentral = ObtenerPalabra(mainLangId, groupId);
            if (string.IsNullOrEmpty(palabraCentral))
            {
                Debug.LogError("No se pudo obtener la palabra central.");
                if (palabraCentralText != null)
                    palabraCentralText.text = "Sin palabra";
                return;
            }

            if (palabraCentralText != null)
                palabraCentralText.text = palabraCentral;
            else
                Debug.LogError("No has asignado 'palabraCentralText' en el Inspector.");

            // 4) Rellenar los 4 botones
            // índice del botón con la VERDAD (traducción correcta)
            indiceVerdadero = UnityEngine.Random.Range(0, 4);

            for (int i = 0; i < 4; i++)
            {
                string langCode = otherLangCodes[i];
                int langId = langIds[langCode];

                string palabraBoton;

                if (i == indiceVerdadero)
                {
                    // VERDAD → palabra del MISMO grupo (traducción correcta)
                    palabraBoton = ObtenerPalabra(langId, groupId);
                }
                else
                {
                    // MENTIRAS → palabras de OTRO grupo (traducciones incorrectas)
                    palabraBoton = ObtenerPalabraDeOtroGrupo(langId, groupId);
                }

                if (botonesIdiomas[i] == null)
                {
                    Debug.LogError($"El botón en la posición {i} no está asignado en el Inspector.");
                    continue;
                }

                TMP_Text textoBoton = botonesIdiomas[i].GetComponentInChildren<TMP_Text>();
                if (textoBoton != null)
                    textoBoton.text = palabraBoton ?? "???";
                else
                    Debug.LogWarning($"El botón {i} no tiene un TMP_Text hijo.");

                int indexCopy = i;
                botonesIdiomas[i].onClick.RemoveAllListeners();
                botonesIdiomas[i].onClick.AddListener(() => OnBotonPulsado(indexCopy));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR en VerdadGame.GenerarRonda: " + ex);
            if (palabraCentralText != null)
                palabraCentralText.text = "Error BD";
        }
    }

    private void OnBotonPulsado(int index)
    {
        // El índice "verdadero" es la traducción correcta
        if (index == indiceVerdadero)
        {
            Debug.Log("Correcto, has encontrado la VERDAD.");
            rachaActual++;

            if (rachaActual > mejorRacha)
            {
                mejorRacha = rachaActual;
                GuardarMejorRacha();
            }
        }
        else
        {
            Debug.Log("Incorrecto, esa palabra NO es la traducción correcta.");
            rachaActual = 0;
        }

        ActualizarUIRachas();
        GenerarRonda(); // Nueva ronda
    }

    // ==========================================================
    //                MÉTODOS DE BASE DE DATOS
    // ==========================================================

    private Dictionary<string, int> ObtenerLanguageIds(IEnumerable<string> codes)
    {
        var originalCodes = codes
            .Where(c => !string.IsNullOrEmpty(c))
            .Distinct()
            .ToList();

        if (originalCodes.Count == 0)
            throw new Exception("Lista de idiomas vacía.");

        var lowerCodes = originalCodes
            .Select(c => c.ToLowerInvariant())
            .Distinct()
            .ToList();

        string placeholders = string.Join(",", lowerCodes.Select(_ => "?"));

        string sql = $@"
            SELECT id, code
            FROM Languages
            WHERE lower(code) IN ({placeholders});
        ";

        var rows = DataBaseManager.DB.Query<Language>(
            sql,
            lowerCodes.Cast<object>().ToArray()
        );

        var mapLowerToId = new Dictionary<string, int>();
        foreach (var row in rows)
        {
            if (!string.IsNullOrEmpty(row.Code))
            {
                string lower = row.Code.ToLowerInvariant();
                if (!mapLowerToId.ContainsKey(lower))
                    mapLowerToId[lower] = row.Id;
            }
        }

        var result = new Dictionary<string, int>();
        foreach (var original in originalCodes)
        {
            string lower = original.ToLowerInvariant();

            if (!mapLowerToId.TryGetValue(lower, out int id))
                throw new Exception($"Idioma no encontrado en la BD: {original}");

            result[original] = id;
        }

        return result;
    }

    /// <summary>
    /// Devuelve un group_id que tenga todas las languages de langIds
    /// intentando que NO sea igual a excludeGroupId.
    /// Si no encuentra ninguno distinto, hace un fallback y permite repetir.
    /// </summary>
    private int ObtenerGrupoConTodosLosIdiomasSinRepetir(List<int> langIds, int excludeGroupId)
    {
        // Primero intentamos buscar un grupo distinto al último (excludeGroupId)
        int groupId = ObtenerGrupoConTodosLosIdiomas(langIds, excludeGroupId);

        if (groupId == -1)
        {
            // Fallback: si no se ha encontrado ninguno distinto,
            // buscamos sin excluir (podría repetirse si sólo hay uno disponible).
            groupId = ObtenerGrupoConTodosLosIdiomas(langIds, -1);
        }

        return groupId;
    }

    /// <summary>
    /// Si excludeGroupId > 0, intenta no devolver ese grupo.
    /// Si excludeGroupId <= 0, ignora la exclusión.
    /// </summary>
    private int ObtenerGrupoConTodosLosIdiomas(List<int> langIds, int excludeGroupId)
    {
        string inClause = string.Join(",", langIds);

        string condicionExclusion = "";
        var parametros = new List<object>();

        // Parámetros comunes (rango y número de idiomas)
        parametros.Add(minGroupId);
        parametros.Add(maxGroupId);
        parametros.Add(langIds.Count);

        if (excludeGroupId > 0)
        {
            condicionExclusion = "AND group_id <> ? ";
        }

        string sql = $@"
            SELECT group_id
            FROM Words
            WHERE language_id IN ({inClause})
              AND group_id BETWEEN ? AND ?
              {condicionExclusion}
            GROUP BY group_id
            HAVING COUNT(DISTINCT language_id) = ?
            ORDER BY RANDOM()
            LIMIT 1;
        ";

        if (excludeGroupId > 0)
        {
            // Orden de parámetros en el SQL: min, max, exclude, langIds.Count
            parametros.Insert(2, excludeGroupId);
        }

        int r = DataBaseManager.DB.ExecuteScalar<int>(sql, parametros.ToArray());

        // Si no devuelve ningún grupo, ExecuteScalar<int> devuelve 0
        if (r == 0)
            return -1;

        return r;
    }

    private string ObtenerPalabra(int langId, int groupId)
    {
        string sql = @"
            SELECT text
            FROM Words
            WHERE language_id = ? AND group_id = ?
            LIMIT 1;
        ";

        return DataBaseManager.DB.ExecuteScalar<string>(sql, langId, groupId);
    }

    private string ObtenerPalabraDeOtroGrupo(int langId, int exGroup)
    {
        string sql = @"
            SELECT text 
            FROM Words
            WHERE language_id = ?
              AND group_id <> ?
              AND group_id BETWEEN ? AND ?
            ORDER BY RANDOM()
            LIMIT 1;
        ";

        return DataBaseManager.DB.ExecuteScalar<string>(
            sql,
            langId,
            exGroup,
            minGroupId,
            maxGroupId
        );
    }
}
