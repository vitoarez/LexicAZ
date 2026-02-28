using UnityEngine;
using UnityEngine.SceneManagement;

public class CategorySelector : MonoBehaviour
{
    // Guarda el nombre de la categoría (por si lo quieres usar en la UI, etc.)
    private const string PREF_CATEGORIA = "selected_category";

    // Claves que usan Mentira, Verdad, Escribe, Repaso
    private const string CATEGORY_MIN_KEY = "category_min_group";
    private const string CATEGORY_MAX_KEY = "category_max_group";

    // Este método se llama desde los botones, con un string distinto según el botón
    public void SelectCategory(string categoryName)
    {
        if (string.IsNullOrEmpty(categoryName))
            categoryName = "todas";

        // 1) Guardamos el nombre tal cual
        PlayerPrefs.SetString(PREF_CATEGORIA, categoryName);

        // 2) Elegimos rango según el nombre
        string key = categoryName.ToLowerInvariant();

        int min = 1;
        int max = 80;

        // Ajusta estos rangos si tu BD usa otros group_id
        if (key.Contains("animal"))          // "Animales", "animals"...
        {
            min = 1;
            max = 25;
        }
        else if (key.Contains("color"))      // "Colores", "colors"...
        {
            min = 26;
            max = 35;
        }
        else if (key.Contains("cuerpo") || key.Contains("body")) // "Partes del cuerpo", "body parts"...
        {
            min = 36;
            max = 50;
        }
        else if (key.Contains("objeto") || key.Contains("object")) // "Objetos", "objects"...
        {
            min = 51;
            max = 80;
        }
        else
        {
            // Cualquier otra cosa -> todas las categorías
            min = 1;
            max = 80;
        }

        PlayerPrefs.SetInt(CATEGORY_MIN_KEY, min);
        PlayerPrefs.SetInt(CATEGORY_MAX_KEY, max);
        PlayerPrefs.Save();

        Debug.Log($"Categoría seleccionada: {categoryName} ({min}-{max})");

        // 3) Ir a la escena de juegos (si quieres que vaya directamente)
        SceneManager.LoadScene("Juegos");
    }
}
