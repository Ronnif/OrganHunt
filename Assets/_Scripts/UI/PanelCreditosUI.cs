using UnityEngine;

public class PanelCreditosUI : MonoBehaviour
{
    [SerializeField] private GameObject panelCreditos;

    void Start()
    {
        panelCreditos.SetActive(false);
    }

    public void AbrirPanel()
    {
        panelCreditos.SetActive(true);
    }

    public void CerrarPanel()
    {
        panelCreditos.SetActive(false);
    }
}