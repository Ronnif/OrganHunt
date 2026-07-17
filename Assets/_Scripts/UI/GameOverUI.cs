using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instancia;

    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private AudioClip sonidoGameOver;

    private AudioSource audioSource;

    void Awake()
    {
        instancia = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Mostrar()
    {
        int puntaje = GameManager.instancia.CalcularPuntaje();
        textoPuntaje.text = $"Puntaje: {puntaje}";
        panelGameOver.SetActive(true);

        if (sonidoGameOver != null)
            audioSource.PlayOneShot(sonidoGameOver);
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}