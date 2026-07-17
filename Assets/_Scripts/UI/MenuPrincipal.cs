using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelSeleccionMapas;

    [Header("Botones de mapas")]
    [SerializeField] private Button btnMapa1;
    [SerializeField] private Button btnMapa2;

    [Header("Escenas")]
    [SerializeField] private string escenaMapa1 = "Level1";
    [SerializeField] private string escenaMapa2 = "Level2";

    void Start()
    {
        panelMenu.SetActive(true);
        panelSeleccionMapas.SetActive(false);

        btnMapa1.interactable = true;
        btnMapa2.interactable = SaveData.Nivel2Desbloqueado;
    }

    public void BotonJugar()
    {
        AudioManager.instancia?.PlayClick();
        panelMenu.SetActive(false);
        panelSeleccionMapas.SetActive(true);

        // Refresca estado por si cambió
        btnMapa2.interactable = SaveData.Nivel2Desbloqueado;
    }

    public void BotonMapa1()
    {
        AudioManager.instancia?.PlayClick();
        AudioManager.instancia?.DetenerMusica();
        FadeManager.instancia.CargarEscena(escenaMapa1);
    }

    public void BotonMapa2()
    {
        AudioManager.instancia?.PlayClick();
        if (!SaveData.Nivel2Desbloqueado) return;

        AudioManager.instancia?.DetenerMusica();
        FadeManager.instancia.CargarEscena(escenaMapa2);
    }

    public void BotonVolver()
    {
        AudioManager.instancia?.PlayClick();
        panelSeleccionMapas.SetActive(false);
        panelMenu.SetActive(true);
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