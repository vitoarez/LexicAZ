using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioUI : MonoBehaviour
{
    // Este método se ejecutará al pulsar el botón "Aprender Vocabulario"
    public void OnEmpezar()
    {
        SceneManager.LoadScene("Juegos");
    }

    // Este método carga la escena "ConfigurarIdiomas"
    public void OnConfigurarIdiomas()
    {
        Debug.Log("Botón Configuración pulsado");
        SceneManager.LoadScene("ConfigurarIdiomas");
    }

    // Este método se ejecutará al pulsar "Salir"
    public void OnSalir()
    {
        Application.Quit();
        Debug.Log("Aplicación cerrada");
    }
}
