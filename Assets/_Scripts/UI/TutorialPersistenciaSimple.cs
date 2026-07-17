using UnityEngine;

public class TutorialPersistenciaSimple : MonoBehaviour
{
    [SerializeField] private GameObject panelTutorial;

    void Start()
    {
        if (SaveData.TutorialVisto)
        {
            panelTutorial.SetActive(false);
        }
        else
        {
            panelTutorial.SetActive(true);
            Time.timeScale = 0f; // pausa mientras se muestra tutorial
        }
    }

    public void CerrarTutorial()
    {
        panelTutorial.SetActive(false);
        SaveData.TutorialVisto = true; // guardar que ya se vio
        Time.timeScale = 1f; // reanudar juego
    }
}