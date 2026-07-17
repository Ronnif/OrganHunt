using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private TextMeshProUGUI textoTitulo;
    [SerializeField] private TextMeshProUGUI textoContenido;
    [SerializeField] private TextMeshProUGUI textoBoton;

    private string[] titulos = {
        "AYUDA 1","AYUDA 2","AYUDA 3","AYUDA 4"
    };

    private string[] mensajes = {
        "El jugador se mueve en 8 direcciones usando WASD o las flechas del teclado.",
        "Presiona J para disparar a los enemigos que encuentres en tu camino.",
        "Recoge los corazones y derrota a los enemigos para poder avanzar al siguiente nivel.",
        "Los órganos que encuentres te otorgarán poderes especiales para ayudarte en tu misión."
    };

    private int indiceActual = 0;

    void Start()
    {
        // NUEVO: si ya vio tutorial, no mostrarlo
        if (SaveData.TutorialVisto)
        {
            panelTutorial.SetActive(false);
            return;
        }

        // Si no lo vio, mostrar y pausar
        panelTutorial.SetActive(true);

        if (PausaManager.instancia != null)
            PausaManager.instancia.PausarPorTutorial();
        else
            Time.timeScale = 0f;

        MostrarTutorial();
    }

    void MostrarTutorial()
    {
        textoTitulo.text = titulos[indiceActual];
        textoContenido.text = mensajes[indiceActual];
        textoBoton.text = (indiceActual == mensajes.Length - 1) ? "Empezar" : "Siguiente";
    }

    public void Siguiente()
    {
        indiceActual++;

        if (indiceActual >= mensajes.Length) CerrarTutorial();
        else MostrarTutorial();
    }

    void CerrarTutorial()
    {
        panelTutorial.SetActive(false);

        // NUEVO: guardar persistencia
        SaveData.TutorialVisto = true;

        if (PausaManager.instancia != null)
            PausaManager.instancia.ReanudarDespuesTutorial();
        else
            Time.timeScale = 1f;
    }

    [ContextMenu("Resetear Tutorial")]
    public void ResetearTutorial()
    {
        PlayerPrefs.DeleteKey("TUTORIAL_VISTO");
        PlayerPrefs.Save();
        Debug.Log("Tutorial reseteado");
    }
}