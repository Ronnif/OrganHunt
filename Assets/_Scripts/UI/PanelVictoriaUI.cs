using UnityEngine;
using TMPro;

public class PanelVictoriaUI : MonoBehaviour
{
    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private string siguienteNivel = "Level2";
    [SerializeField] private string escenaInicio = "MainMenu";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxNivelCompletado;

    public void Mostrar()
    {
        int puntaje = (GameManager.instancia != null) ? GameManager.instancia.CalcularPuntaje() : 0;
        if (textoPuntaje != null)
            textoPuntaje.text = $"PUNTAJE - {puntaje}";

        if (audioSource != null && sfxNivelCompletado != null)
            audioSource.PlayOneShot(sfxNivelCompletado);

        Time.timeScale = 0f;
        panelVictoria.SetActive(true);
    }

    public void Continuar()
    {
        Time.timeScale = 1f;
        FadeManager.instancia.CargarEscena(siguienteNivel);
    }

    public void VolverAlInicio()
    {
        Time.timeScale = 1f;
        FadeManager.instancia.CargarEscena(escenaInicio);
    }
}