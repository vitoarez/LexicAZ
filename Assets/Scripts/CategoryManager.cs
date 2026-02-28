using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    // Claves para PlayerPrefs
    private const string CATEGORY_MIN_KEY = "category_min_group";
    private const string CATEGORY_MAX_KEY = "category_max_group";

    // Animales: grupos 1-25
    public void SelectAnimals()
    {
        PlayerPrefs.SetInt(CATEGORY_MIN_KEY, 1);
        PlayerPrefs.SetInt(CATEGORY_MAX_KEY, 25);
        PlayerPrefs.Save();
        Debug.Log("Categoría seleccionada: Animales (1-25)");
    }

    // Colores: grupos 26-35
    public void SelectColors()
    {
        PlayerPrefs.SetInt(CATEGORY_MIN_KEY, 26);
        PlayerPrefs.SetInt(CATEGORY_MAX_KEY, 35);
        PlayerPrefs.Save();
        Debug.Log("Categoría seleccionada: Colores (26-35)");
    }

    // Objetos: grupos 51-80
    public void SelectObjects()
    {
        PlayerPrefs.SetInt(CATEGORY_MIN_KEY, 51);
        PlayerPrefs.SetInt(CATEGORY_MAX_KEY, 80);
        PlayerPrefs.Save();
        Debug.Log("Categoría seleccionada: Objetos (51-80)");
    }

    // Partes del cuerpo: grupos 36-50
    public void SelectBodyParts()
    {
        PlayerPrefs.SetInt(CATEGORY_MIN_KEY, 36);
        PlayerPrefs.SetInt(CATEGORY_MAX_KEY, 50);
        PlayerPrefs.Save();
        Debug.Log("Categoría seleccionada: Partes del cuerpo (36-50)");
    }
}
