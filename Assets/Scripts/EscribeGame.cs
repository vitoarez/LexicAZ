using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using SQLite4Unity3d;

public class EscribeGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text[] textosIdiomas;     // 4 textos: idiomas 2,3,4,5
    [SerializeField] private TMP_InputField inputRespuesta;
    [SerializeField] private TMP_Text feedbackText;

    [Header("Rachas")]
    [SerializeField] private TMP_Text rachaNumeroText;        // SOLO número de racha actual
    [SerializeField] private TMP_Text mejorRachaNumeroText;   // SOLO número de mejor racha

    private string[] languageCodes;           // orden de idiomas (es,en,it,fr,de,nl)
    private string idiomaObjetivoCode;        // idioma 1 (donde hay que escribir)
    private string respuestaCorrecta;         // palabra correcta en idioma 1

    // Rango de grupos para la categoría seleccionada (igual que Mentira/Verdad)
    private int minGroupId = 1;
    private int maxGroupId = 80;

    // Rachas
    private int rachaActual = 0;              // Contador de aciertos seguidos
    private int mejorRacha = 0;               // Mejor racha histórica

    // Recordar el último grupo para no repetir la misma palabra dos veces seguidas
    private int ultimoGrupoId = -1;

    // Claves PlayerPrefs para categoría por rango y mejor racha
    private const string CATEGORY_MIN_KEY = "category_min_group";
    private const string CATEGORY_MAX_KEY = "category_max_group";
    private const string PREF_MEJOR_RACHA = "EscribeGame_BestStreak";

    private void Start()
    {
        CargarOrdenIdiomas();
        CargarCategoriaActual();
        CargarMejorRacha();
        ActualizarRachaUI();
        ActualizarMejorRachaUI();
        GenerarRonda();
    }

    private void CargarOrdenIdiomas()
    {
        string order = PlayerPrefs.GetString("language_order", "es,en,it,fr,de,nl");
        languageCodes = order.Split(',');

        if (languageCodes.Length < 5)
        {
            Debug.LogError("Se necesitan al menos 5 idiomas en language_order para EscribeGame.");
            languageCodes = new[] { "es", "en", "it", "fr", "de" };
        }

        idiomaObjetivoCode = languageCodes[0];   // idioma en el que el jugador debe escribir
        Debug.Log("Idioma objetivo (EscribeGame): " + idiomaObjetivoCode);
    }

    private void CargarCategoriaActual()
    {
        minGroupId = PlayerPrefs.GetInt(CATEGORY_MIN_KEY, 1);
        maxGroupId = PlayerPrefs.GetInt(CATEGORY_MAX_KEY, 80);
        Debug.Log($"EscribeGame - Rango de grupos: {minGroupId} a {maxGroupId}");
    }

    private void CargarMejorRacha()
    {
        mejorRacha = PlayerPrefs.GetInt(PREF_MEJOR_RACHA, 0);
        Debug.Log("Mejor racha (Escribe) cargada: " + mejorRacha);
    }

    private void GuardarMejorRacha()
    {
        PlayerPrefs.SetInt(PREF_MEJOR_RACHA, mejorRacha);
        PlayerPrefs.Save();
        Debug.Log("Mejor racha (Escribe) guardada: " + mejorRacha);
    }

    public void GenerarRonda()
    {
        if (inputRespuesta != null)
            inputRespuesta.text = "";

        if (languageCodes == null || languageCodes.Length < 5)
        {
            Debug.LogError("Se necesitan al menos 5 idiomas en language_order para EscribeGame.");
            if (feedbackText != null)
                feedbackText.text = "Configura al menos 5 idiomas.";
            return;
        }

        if (textosIdiomas == null || textosIdiomas.Length < 4)
        {
            Debug.LogError("Asigna 4 TMP_Text en textosIdiomas en el Inspector.");
            return;
        }

        try
        {
            // Idiomas: objetivo + 4 mostrados
            string mainLangCode = languageCodes[0];
            string[] otherLangCodes = languageCodes.Skip(1).Take(4).ToArray();

            // 1) Obtener IDs de idiomas (como en Mentira/Verdad)
            var codigos = new List<string> { mainLangCode };
            codigos.AddRange(otherLangCodes);
            Dictionary<string, int> langIds = ObtenerLanguageIds(codigos);

            // 2) Escoger un grupo que tenga palabras en los 5 idiomas, sin repetir el anterior si es posible
            int groupId = ObtenerGrupoConTodosLosIdiomasSinRepetir(langIds.Values.ToList(), ultimoGrupoId);
            if (groupId == -1)
            {
                Debug.LogWarning("No se encontró ningún grupo con todos los idiomas en el rango seleccionado.");
                if (feedbackText != null)
                    feedbackText.text = "No hay suficientes palabras en esta categoría.";
                return;
            }

            // Guardamos el grupo actual para no repetir
            ultimoGrupoId = groupId;

            // 3) Guardar la respuesta correcta (idioma objetivo)
            int mainLangId = langIds[mainLangCode];
            respuestaCorrecta = ObtenerPalabra(mainLangId, groupId);

            if (string.IsNullOrEmpty(respuestaCorrecta))
            {
                Debug.LogWarning("No hay palabra en el idioma objetivo para este grupo.");
                if (feedbackText != null)
                    feedbackText.text = "Sin palabra en idioma objetivo.";
                return;
            }

            // 4) Mostrar las palabras en los idiomas 2,3,4 y 5
            for (int i = 0; i < 4; i++)
            {
                string code = otherLangCodes[i];
                int langId = langIds[code];

                string texto = ObtenerPalabra(langId, groupId);
                if (string.IsNullOrEmpty(texto))
                    textosIdiomas[i].text = "---";
                else
                    textosIdiomas[i].text = texto;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR en EscribeGame.GenerarRonda: " + ex);
            if (feedbackText != null)
                feedbackText.text = "Error BD";
        }
    }

    public void ComprobarRespuesta()
    {
        if (inputRespuesta == null || feedbackText == null)
            return;

        string respuestaJugador = inputRespuesta.text;

        if (string.IsNullOrWhiteSpace(respuestaJugador) || string.IsNullOrEmpty(respuestaCorrecta))
        {
            feedbackText.text = "Escribe una respuesta.";
            return;
        }

        // Normalizar (ignorar mayúsculas/minúsculas y espacios extremos)
        string r1 = Normalizar(respuestaJugador);
        string r2 = Normalizar(respuestaCorrecta);

        if (r1 == r2)
        {
            // ACIERTO
            rachaActual++;

            if (rachaActual > mejorRacha)
            {
                mejorRacha = rachaActual;
                GuardarMejorRacha();
                ActualizarMejorRachaUI();
            }

            // Mensaje de OK! (como querías)
            feedbackText.text = "OK!";
        }
        else
        {
            // FALLO, se resetea la racha
            rachaActual = 0;

            // Mostrar la palabra correcta entre X, sin espacios: XpalabraX
            feedbackText.text = $"X {respuestaCorrecta} X";
        }

        ActualizarRachaUI();

        // Misma lógica que tenías tú: pasa a la siguiente ronda inmediatamente
        GenerarRonda();
    }

    private void ActualizarRachaUI()
    {
        if (rachaNumeroText != null)
        {
            rachaNumeroText.text = rachaActual.ToString();
        }
        else
        {
            Debug.LogWarning("No se ha asignado 'rachaNumeroText' en el Inspector (EscribeGame).");
        }
    }

    private void ActualizarMejorRachaUI()
    {
        if (mejorRachaNumeroText != null)
        {
            mejorRachaNumeroText.text = mejorRacha.ToString();
        }
        else
        {
            Debug.LogWarning("No se ha asignado 'mejorRachaNumeroText' en el Inspector (EscribeGame).");
        }
    }

    private string Normalizar(string s)
    {
        if (string.IsNullOrEmpty(s))
            return "";

        return s.Trim().ToLowerInvariant();
    }

    // Botón "Siguiente" si lo usas
    public void OnClickSiguiente()
    {
        GenerarRonda();
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

        string sql = @"
            SELECT id, code
            FROM Languages
            WHERE lower(code) IN (" + placeholders + @");
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
                throw new Exception($" Idioma no encontrado en la BD: {original}");

            result[original] = id;
        }

        return result;
    }

    private int ObtenerGrupoConTodosLosIdiomasSinRepetir(List<int> langIds, int excludeGroupId)
    {
        int groupId = ObtenerGrupoConTodosLosIdiomas(langIds, excludeGroupId);

        if (groupId == -1)
        {
            groupId = ObtenerGrupoConTodosLosIdiomas(langIds, -1);
        }

        return groupId;
    }

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

        string sql = @"
            SELECT group_id
            FROM Words
            WHERE language_id IN (" + inClause + @")
              AND group_id BETWEEN ? AND ?
              " + condicionExclusion + @"
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
}
