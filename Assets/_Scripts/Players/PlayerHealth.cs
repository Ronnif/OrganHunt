using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    [SerializeField] private TextMeshProUGUI textoVida;

    [Header("Efectos visuales")]
    [SerializeField] private TextoFlotanteVida textoFlotante;

    [Header("Reaparición")]
    [SerializeField] private Transform cuartoSeguro;
    [SerializeField] private float duracionInvulnerabilidad = 2f;

    [Header("Efecto de daño")]
    [SerializeField] private float duracionParpadeo = 0.15f;
    [SerializeField] private int cantidadParpadeos = 3;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoRecibirDano;
    [SerializeField] private AudioClip sonidoPerderVida;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool esInvulnerable = false;

    void Start()
    {
        vidaActual = vidaMaxima;
        reaparicionesActuales = reaparicionesMaximas;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        ActualizarHUD();
    }

    public void RecibirDano(float cantidad)
    {
        if (vidaActual <= 0 || esInvulnerable) return;

        vidaActual = Mathf.Clamp(vidaActual - cantidad, 0, vidaMaxima);
        ActualizarHUD();
        StartCoroutine(EfectoParpadeoDano());

        if (sonidoRecibirDano != null)
            audioSource.PlayOneShot(sonidoRecibirDano);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Curar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMaxima);
        ActualizarHUD();
    }

    public void AumentarVidaMaxima(float porcentaje)
    {
        float aumento = vidaMaxima * (porcentaje / 100f);
        vidaMaxima += aumento;
        vidaActual = vidaMaxima;
        ActualizarHUD();

        if (textoFlotante != null)
            textoFlotante.Mostrar($"+{Mathf.RoundToInt(aumento)}");
    }

    private void Morir()
    {
        reaparicionesActuales--;

        if (sonidoPerderVida != null)
            audioSource.PlayOneShot(sonidoPerderVida);

        if (reaparicionesActuales <= 0)
        {
            Debug.Log("GAME OVER - Perdiste todas las reapariciones");
        }
        else
        {
            Debug.Log("Reapareciendo... vidas restantes: " + reaparicionesActuales);
            vidaActual = vidaMaxima;

            if (cuartoSeguro != null)
                transform.position = cuartoSeguro.position;

            StartCoroutine(InvulnerabilidadTemporal());
        }

        ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        if (barraVidaRelleno != null)
            barraVidaRelleno.fillAmount = vidaActual / vidaMaxima;

        if (textoReapariciones != null)
            textoReapariciones.text = $"REAPARICIONES: {reaparicionesActuales}/{reaparicionesMaximas}";

        if (textoVida != null)
            textoVida.text = $"{Mathf.RoundToInt(vidaActual)}/{Mathf.RoundToInt(vidaMaxima)}";
    }

    private IEnumerator EfectoParpadeoDano()
    {
        for (int i = 0; i < cantidadParpadeos; i++)
        {
            spriteRenderer.color = new Color(1f, 0.3f, 0.3f, 0.5f);
            yield return new WaitForSeconds(duracionParpadeo);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(duracionParpadeo);
        }
    }

    private IEnumerator InvulnerabilidadTemporal()
    {
        esInvulnerable = true;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionInvulnerabilidad)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);

            tiempoTranscurrido += 0.2f;
        }

        spriteRenderer.color = Color.white;
        esInvulnerable = false;
    }
}