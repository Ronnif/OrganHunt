using UnityEngine;

public class PuertaSalida : MonoBehaviour
{
    [SerializeField] private PanelVictoriaUI panelVictoriaUI;
    private bool activado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activado) return;
        if (!other.CompareTag("Player")) return;

        if (GameManager.instancia != null && GameManager.instancia.NivelCompletado())
        {
            activado = true;

            // DESBLOQUEAR MAPA 2
            SaveData.Nivel2Desbloqueado = true;

            Debug.Log("¡Nivel completado! Mostrando panel de victoria...");
            panelVictoriaUI.Mostrar();
        }
        else
        {
            Debug.Log("Aún faltan requisitos para pasar de nivel.");
        }
    }
}