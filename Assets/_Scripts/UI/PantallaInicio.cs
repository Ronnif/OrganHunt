using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PantallaInicio : MonoBehaviour
{
    private bool cargando = false;

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && !cargando)
        {
            cargando = true;
            FadeManager.instancia.CargarEscena("MainMenu");
        }
    }
}