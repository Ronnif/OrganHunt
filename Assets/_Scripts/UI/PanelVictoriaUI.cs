using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PanelVictoriaUI : MonoBehaviour
{
    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private TextMeshProUGUI textoMensaje;
    [SerializeField] private GameObject btnContinuar;
    [SerializeField] private GameObject btnVolverInicio;
    [SerializeField] private string siguienteNivel = "Level2";
    [SerializeField] private string escenaInicio = "MainMenu";

    [Header("Configuración de este nivel")]
    [SerializeField] private bool esUltimoNivel = false;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxNivelCompletado;

    public void Mostrar()
    {
        int puntaje = (GameManager.instancia != null) ? GameManager.instancia.CalcularPuntaje() : 0;
        if (textoPuntaje != null)
            textoPuntaje.text = $"PUNTAJE - {puntaje}";

        if (!esUltimoNivel)
        {
            if (textoMensaje != null) textoMensaje.text = "¡NIVEL COMPLETADO!";
            if (btnContinuar != null) btnContinuar.SetActive(true);
        }
        else
        {
            if (textoMensaje != null) textoMensaje.text = "¡JUEGO COMPLETADO!";
            if (btnContinuar != null) btnContinuar.SetActive(false);
        }

        if (btnVolverInicio != null) btnVolverInicio.SetActive(true);

        if (PausaManager.instancia != null)
            PausaManager.instancia.BloquearPausa();

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