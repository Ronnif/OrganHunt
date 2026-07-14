using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;

    [Header("Reapariciones")]
    [SerializeField] private int reaparicionesMaximas = 3;
    private int reaparicionesActuales;

    [Header("Referencias HUD")]
    [SerializeField] private Image barraVidaRelleno;
    [SerializeField] private TextMeshProUGUI textoReapariciones;

    [Header("Reaparición")]
    [SerializeField] private Transform cuartoSeguro;

    void Start()
    {
        vidaActual = vidaMaxima;
        reaparicionesActuales = reaparicionesMaximas;
        ActualizarHUD();
    }

    /// <summary>
    /// Aplica daño al jugador. Si la vida llega a 0, dispara la muerte/reaparición.
    /// </summary>
    public void RecibirDano(float cantidad)
    {
        if (vidaActual <= 0) return;

        vidaActual = Mathf.Clamp(vidaActual - cantidad, 0, vidaMaxima);
        ActualizarHUD();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    /// <summary>
    /// Restaura vida al jugador (ej. al recoger un corazón).
    /// </summary>
    public void Curar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMaxima);
        ActualizarHUD();
    }

    /// <summary>
    /// Maneja la pérdida de una reaparición y el reinicio de vida,
    /// o el Game Over si ya no quedan reapariciones.
    /// </summary>
    private void Morir()
    {
        reaparicionesActuales--;

        if (reaparicionesActuales <= 0)
        {
            Debug.Log("GAME OVER - Perdiste todas las reapariciones");
            // TODO: cargar pantalla de Game Over
        }
        else
        {
            Debug.Log("Reapareciendo... vidas restantes: " + reaparicionesActuales);
            vidaActual = vidaMaxima;

            if (cuartoSeguro != null)
                transform.position = cuartoSeguro.position;
        }

        ActualizarHUD();
    }

    /// <summary>
    /// Sincroniza la barra de vida y el texto de reapariciones con los valores actuales.
    /// </summary>
    private void ActualizarHUD()
    {
        if (barraVidaRelleno != null)
            barraVidaRelleno.fillAmount = vidaActual / vidaMaxima;

        if (textoReapariciones != null)
            textoReapariciones.text = $"REAPARICIONES: {reaparicionesActuales}/{reaparicionesMaximas}";
    }
}