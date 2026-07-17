using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Progreso del nivel")]
    [SerializeField] private int corazonesTotales;
    private int corazonesRecolectados = 0;

    [SerializeField] private int enemigosTotales;
    private int enemigosDerrotados = 0;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI textoCorazones;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ActualizarHUD();
    }

    public void RecolectarCorazon()
    {
        corazonesRecolectados++;
        ActualizarHUD();
    }

    public void RegistrarEnemigoDerrotado()
    {
        enemigosDerrotados++;
    }

    public bool NivelCompletado()
    {
        return corazonesRecolectados >= corazonesTotales && enemigosDerrotados >= enemigosTotales;
    }

    public int CalcularPuntaje()
    {
        int puntaje = (corazonesRecolectados * 10) + (enemigosDerrotados * 25);
        if (NivelCompletado())
            puntaje += 50;
        return puntaje;
    }

    void ActualizarHUD()
    {
        if (textoCorazones != null)
            textoCorazones.text = $"CORAZONES: {corazonesRecolectados}/{corazonesTotales}";
    }
}