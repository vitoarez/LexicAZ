using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;

public class MentiraGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text palabraCentralText;
    [SerializeField] private Button[] botonesIdiomas;

    [Header("Rachas")]
    [SerializeField] private TMP_Text rachaNumeroText;
    [SerializeField] private TMP_Text mejorRachaNumeroText;

    private string[] languageCodes;
    private int indiceCorrecto;

    private int rachaActual = 0;
    private int mejorRacha = 0;

    private int minGroupId = 1;
    private int maxGroupId = 80;

    // Recordar el último grupo usado para no repetir palabra central
    private int ultimoGrupoId = -1;

    private const string PREF_MEJOR_RACHA = "MentiraGame_BestStreak";
    private const string CATEGORY_MIN_KEY = "category_min_group";
    private const string CATEGORY_MAX_KEY = "category_max_group";

    private void Start()
    {
        CargarOrdenIdiomas();

        minGroupId = PlayerPrefs.GetInt(CATEGORY_MIN_KEY, 1);
        maxGroupId = PlayerPrefs.GetInt(CATEGORY_MAX_KEY, 80);
        Debug.Log($"MentiraGame — Rango grupos: {minGroupId} a {maxGroupId}");

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
            Debug.LogWarning("Se necesitan 5 idiomas para MentiraGame.");
        }
    }

    private void CargarMejorRacha()
    {
        mejorRacha = PlayerPrefs.GetInt(PREF_MEJOR_RACHA, 0);
    }

    private void GuardarMejorRacha()
    {
        PlayerPrefs.SetInt(PREF_MEJOR_RACHA, mejorRacha);
        PlayerPrefs.Save();
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
                palabraCentralText.text = "Error idiomas";
                return;
            }

            string mainLang = languageCodes[0];
            string[] otherLangs = languageCodes.Skip(1).Take(4).ToArray();

            Dictionary<string, int> langIds = ObtenerLanguageIds(otherLangs.Prepend(mainLang));

            // Grupo con todos los idiomas, intentando no repetir el último
            int groupId = ObtenerGrupoConTodosLosIdiomasSinRepetir(langIds.Values.ToList(), ultimoGrupoId);

            if (groupId == -1)
            {
                palabraCentralText.text = "Sin grupos";
                Debug.LogWarning("MentiraGame: no se ha encontrado ningún grupo con todos los idiomas requeridos en el rango.");
                return;
            }

            // Guardamos para la próxima ronda
            ultimoGrupoId = groupId;

            string palabraCentral = ObtenerPalabra(langIds[mainLang], groupId);

            if (string.IsNullOrEmpty(palabraCentral))
            {
                palabraCentralText.text = "Sin palabra";
                return;
            }

            palabraCentralText.text = palabraCentral;

            // Índice del botón que será la MENTIRA (la incorrecta)
            indiceCorrecto = UnityEngine.Random.Range(0, 4);

            for (int i = 0; i < 4; i++)
            {
                string langCode = otherLangs[i];
                int langId = langIds[langCode];

                string palabraBoton;

                if (i == indiceCorrecto)
                {
                    // Este es el botón que el jugador debe pulsar:
                    // LA MENTIRA → mala traducción → palabra de OTRO grupo
                    palabraBoton = ObtenerPalabraDeOtroGrupo(langId, groupId);
                }
                else
                {
                    // 🔹 Estos son los 3 botones con traducciones CORRECTAS (verdades)
                    // → misma group_id que la palabra central
                    palabraBoton = ObtenerPalabra(langId, groupId);
                }

                TMP_Text t = botonesIdiomas[i].GetComponentInChildren<TMP_Text>();
                t.text = palabraBoton ?? "???";

                int idx = i;
                botonesIdiomas[i].onClick.RemoveAllListeners();
                botonesIdiomas[i].onClick.AddListener(() => OnBotonPulsado(idx));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("ERROR GENERANDO RONDA (MentiraGame): " + ex);
            if (palabraCentralText != null)
                palabraCentralText.text = "Error BD";
        }
    }

    private void OnBotonPulsado(int index)
    {
        // El índice "correcto" es la mentira (la mala traducción)
        if (index == indiceCorrecto)
        {
            rachaActual++;

            if (rachaActual > mejorRacha)
            {
                mejorRacha = rachaActual;
                GuardarMejorRacha();
            }
        }
        else
        {
            rachaActual = 0;
        }

        ActualizarUIRachas();
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
            // Insertamos el excludeGroupId como último parámetro antes del count
            // Orden: min, max, exclude, langIds.Count
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
