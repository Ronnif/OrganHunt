using UnityEngine;
using UnityEngine.UI;

public class PanelConfiguracionUI : MonoBehaviour
{
    [SerializeField] private GameObject panelConfiguracion;
    [SerializeField] private Toggle toggleMusica;
    [SerializeField] private Toggle toggleSonido;

    void Start()
    {
        toggleMusica.onValueChanged.AddListener(OnToggleMusica);
        toggleSonido.onValueChanged.AddListener(OnToggleSonido);
    }

    void OnToggleMusica(bool activar)
    {
        if (AudioManager.instancia != null)
            AudioManager.instancia.SetVolumenMusica(activar);
    }

    void OnToggleSonido(bool activar)
    {
        if (AudioManager.instancia != null)
            AudioManager.instancia.SetVolumenSFX(activar);
    }

    public void AbrirPanel()
    {
        panelConfiguracion.SetActive(true);
    }

    public void CerrarPanel()
    {
        panelConfiguracion.SetActive(false);
    }
}