using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaSalida : MonoBehaviour
{
    [SerializeField] private string siguienteNivel = "Level2";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instancia.NivelCompletado())
            {
                Debug.Log("¡Nivel completado! Cargando siguiente nivel...");
                FadeManager.instancia.CargarEscena(siguienteNivel);
            }
            else
            {
                Debug.Log("Aún faltan requisitos para pasar de nivel.");
            }
        }
    }
}