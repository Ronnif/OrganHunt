using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour
{
    public static PausaManager instancia;

    [Header("UI")]
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private string nombreEscenaMenu = "MainMenu";

    public bool estaPausado { get; private set; }
    private bool bloqueado = false;

    private void Awake()
    {
        if (instancia == null) instancia = this;
        else { Destroy(gameObject); return; }
    }

    private void Start()
    {
        if (panelTutorial != null && panelTutorial.activeInHierarchy)
            PausarPorTutorial();
        else
            Reanudar();
    }

    private void Update()
    {
        if (bloqueado) return;

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

    public void PausarPorTutorial()
    {
        estaPausado = true;
        Time.timeScale = 0f;
        if (panelPausa != null) panelPausa.SetActive(false);
    }

    public void ReanudarDespuesTutorial()
    {
        estaPausado = false;
        Time.timeScale = 1f;
    }

    public void BloquearPausa()
    {
        bloqueado = true;
        estaPausado = true;
        Time.timeScale = 0f;
        if (panelPausa != null) panelPausa.SetActive(false);
    }

    public void DesbloquearPausa()
    {
        bloqueado = false;
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