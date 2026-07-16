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
        VerificarProgreso();
    }

    public void RegistrarEnemigoDerrotado()
    {
        enemigosDerrotados++;
        VerificarProgreso();
    }

    void ActualizarHUD()
    {
        if (textoCorazones != null)
            textoCorazones.text = $"CORAZONES: {corazonesRecolectados}/{corazonesTotales}";
    }

    void VerificarProgreso()
    {
        if (corazonesRecolectados >= corazonesTotales && enemigosDerrotados >= enemigosTotales)
        {
            Debug.Log("¡Requisitos completos! Puedes salir del nivel.");
        }
    }
}