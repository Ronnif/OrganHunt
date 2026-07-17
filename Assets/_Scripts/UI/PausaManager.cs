using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour
{
    public static PausaManager instancia;

    [Header("UI")]
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject panelTutorial; // <- asignar en Inspector
    [SerializeField] private string nombreEscenaMenu = "MainMenu";

    public bool estaPausado { get; private set; }

    private void Awake()
    {
        if (instancia == null) instancia = this;
        else { Destroy(gameObject); return; }
    }

    private void Start()
    {
        // Si el tutorial inicia activo, pausamos de una vez
        if (panelTutorial != null && panelTutorial.activeInHierarchy)
            PausarPorTutorial();
        else
            Reanudar();
    }

    private void Update()
    {
        // Si tutorial está abierto, bloquear tecla ESC para pausa normal
        if (panelTutorial != null && panelTutorial.activeInHierarchy)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado) Reanudar();
            else Pausar();
        }
    }

    public void Pausar()
    {
        estaPausado = true;
        Time.timeScale = 0f;
        if (panelPausa != null) panelPausa.SetActive(true);
    }

    public void Reanudar()
    {
        estaPausado = false;
        Time.timeScale = 1f;
        if (panelPausa != null) panelPausa.SetActive(false);
    }

    // Llamar cuando abras el tutorial
    public void PausarPorTutorial()
    {
        estaPausado = true;
        Time.timeScale = 0f;
        if (panelPausa != null) panelPausa.SetActive(false); // evita choque visual
    }

    // Llamar cuando termine/cierres tutorial
    public void ReanudarDespuesTutorial()
    {
        estaPausado = false;
        Time.timeScale = 1f;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaMenu);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}