using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LanguageOrderUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public Transform panel;           // Contenedor donde se instancian los items (LanguagesPanel)
    public GameObject languagePrefab; // Prefab del elemento de idioma (LanguageItem)

    private List<Language> languages = new List<Language>();

    private const string LANGUAGE_ORDER_KEY = "language_order";

    void Start()
    {
        InicializarIdiomasBase();     // Crea los 6 idiomas
        LoadSavedOrderIfAny();        // Reordena según PlayerPrefs (o crea orden por defecto)
        GenerateLanguageItems();      // Genera los botones en la UI
    }

    /// <summary>
    /// Crea los 6 idiomas básicos sin depender de la base de datos.
    /// </summary>
    void InicializarIdiomasBase()
    {
        languages = new List<Language>
        {
            new Language { Id = 1, Name = "Español",  Code = "es" },
            new Language { Id = 2, Name = "English",  Code = "en" },
            new Language { Id = 3, Name = "Italiano", Code = "it" },
            new Language { Id = 4, Name = "Français", Code = "fr" },
            new Language { Id = 5, Name = "Deutsch",  Code = "de" },
            new Language { Id = 6, Name = "Nederlands", Code = "nl" },
        };

        Debug.Log("InicializarIdiomasBase: idiomas cargados = " + languages.Count);
        foreach (var lang in languages)
        {
            Debug.Log($"Idioma base: {lang.Name} ({lang.Code}), Id = {lang.Id}");
        }
    }

    /// <summary>
    /// Reordena 'languages' según PlayerPrefs("language_order").
    /// Si no existe, crea un orden por defecto y lo guarda.
    /// </summary>
    void LoadSavedOrderIfAny()
    {
        string saved = PlayerPrefs.GetString(LANGUAGE_ORDER_KEY, "");

        if (string.IsNullOrEmpty(saved))
        {
            // Orden por defecto con tus 6 idiomas
            saved = "es,en,it,fr,de,nl";
            PlayerPrefs.SetString(LANGUAGE_ORDER_KEY, saved);
            PlayerPrefs.Save();

            Debug.Log("No había language_order guardado. Se crea orden por defecto: " + saved);
        }
        else
        {
            Debug.Log("language_order leído de PlayerPrefs: " + saved);
        }

        var codesOrder = new List<string>(saved.Split(','));

        // Mapa code -> Language
        var map = languages.ToDictionary(l => l.Code);

        var newList = new List<Language>();

        // Añadimos en el orden guardado
        foreach (var code in codesOrder)
        {
            if (map.TryGetValue(code, out var lang))
            {
                newList.Add(lang);
            }
        }

        // Por si algún idioma base no estaba en el guardado
        foreach (var lang in languages)
        {
            if (!newList.Contains(lang))
            {
                newList.Add(lang);
            }
        }

        languages = newList;

        Debug.Log("Languages reordenado. Total idiomas: " + languages.Count);
    }

    void GenerateLanguageItems()
    {
        // Limpia los listeners antiguos y destruye los items actuales
        foreach (Transform child in panel)
        {
            var upBtn = child.Find("UpButton")?.GetComponent<Button>();
            var downBtn = child.Find("DownButton")?.GetComponent<Button>();
            if (upBtn != null) upBtn.onClick.RemoveAllListeners();
            if (downBtn != null) downBtn.onClick.RemoveAllListeners();

            Destroy(child.gameObject);
        }

        Debug.Log("GenerateLanguageItems: generando " + languages.Count + " items de idioma.");

        // Crea un nuevo elemento por cada idioma
        for (int i = 0; i < languages.Count; i++)
        {
            var lang = languages[i];
            var item = Instantiate(languagePrefab, panel);

            // Actualiza el texto visible
            var text = item.transform.Find("LanguageName")?.GetComponent<TMP_Text>();
            if (text != null)
            {
                text.text = lang.Name;
            }
            else
            {
                Debug.LogWarning("No se encontró el componente TMP_Text 'LanguageName' en el prefab.");
            }

            // Añade funcionalidad a los botones ↑ ↓
            int index = i;
            var upButton = item.transform.Find("UpButton")?.GetComponent<Button>();
            var downButton = item.transform.Find("DownButton")?.GetComponent<Button>();

            if (upButton != null)
                upButton.onClick.AddListener(() => MoveUp(index));
            else
                Debug.LogWarning("No se encontró 'UpButton' en el prefab.");

            if (downButton != null)
                downButton.onClick.AddListener(() => MoveDown(index));
            else
                Debug.LogWarning("No se encontró 'DownButton' en el prefab.");
        }

        // Guardamos el nuevo orden en PlayerPrefs
        SaveLanguageOrder();

        // Aplica el idioma del primer elemento automáticamente
        ApplyFirstLanguageAsAppLanguage();
    }

    void MoveUp(int index)
    {
        if (index <= 0) return;

        (languages[index - 1], languages[index]) = (languages[index], languages[index - 1]);
        GenerateLanguageItems();
    }

    void MoveDown(int index)
    {
        if (index >= languages.Count - 1) return;

        (languages[index + 1], languages[index]) = (languages[index], languages[index + 1]);
        GenerateLanguageItems();
    }

    // Guarda en PlayerPrefs el orden actual (ej: "en,es,it,fr,de,nl")
    void SaveLanguageOrder()
    {
        var codes = new List<string>();
        foreach (var lang in languages)
        {
            codes.Add(lang.Code);
        }

        string order = string.Join(",", codes);
        PlayerPrefs.SetString(LANGUAGE_ORDER_KEY, order);
        PlayerPrefs.Save();

        Debug.Log("language_order guardado: " + order);
    }

    // Cambia el idioma de la aplicación al que esté en primer lugar en la lista
    public void ApplyFirstLanguageAsAppLanguage()
    {
        if (languages.Count == 0)
            return;

        string topLanguageName = languages[0].Name;
        string localeCode = GetLocaleCodeFromName(topLanguageName);

        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.Code == localeCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log($"Idioma cambiado automáticamente a: {locale.Identifier.Code}");
                return;
            }
        }

        Debug.LogWarning($"No se encontró un Locale para el idioma: {topLanguageName}");
    }

    // Convierte el nombre del idioma en su código ISO (para los Locales de Unity)
    private string GetLocaleCodeFromName(string languageName)
    {
        switch (languageName.ToLower())
        {
            case "español":   return "es";
            case "english":   return "en";
            case "italiano":  return "it";
            case "français":  return "fr";
            case "deutsch":   return "de";
            case "nederlands":return "nl";
            default:          return "en"; // idioma por defecto
        }
    }

    public void GoToInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
