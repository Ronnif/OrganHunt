using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void BotonJugar()
    {
        AudioManager.instancia?.PlayClick();
        FadeManager.instancia.CargarEscena("Level1");
    }

    public void BotonConfiguracion()
    {
        AudioManager.instancia?.PlayClick();
        Debug.Log("Configuración");
    }

    public void BotonCreditos()
    {
        AudioManager.instancia?.PlayClick();
        Debug.Log("Créditos");
    }

    public void BotonSalir()
    {
        AudioManager.instancia?.PlayClick();
        Application.Quit();
        Debug.Log("Salir");
    }
}