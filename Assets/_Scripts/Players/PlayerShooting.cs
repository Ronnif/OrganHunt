using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float cadenciaDisparo = 0.3f;
    [SerializeField] private AudioClip sonidoDisparo;
    private float tiempoUltimoDisparo;
    private Vector2 ultimaDireccion = Vector2.down;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Proyectil.multiplicadorDano = 1f; // Resetea el bonus del Cerebro al iniciar/reiniciar el nivel
    }

    void Update()
    {
        ActualizarDireccion();
        if (Keyboard.current.jKey.isPressed && Time.time >= tiempoUltimoDisparo + cadenciaDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void ActualizarDireccion()
    {
        Keyboard kb = Keyboard.current;
        float moveX = (kb.dKey.isPressed || kb.rightArrowKey.isPressed ? 1f : 0f)
                    - (kb.aKey.isPressed || kb.leftArrowKey.isPressed ? 1f : 0f);
        float moveY = (kb.wKey.isPressed || kb.upArrowKey.isPressed ? 1f : 0f)
                    - (kb.sKey.isPressed || kb.downArrowKey.isPressed ? 1f : 0f);
        Vector2 direccionActual = new Vector2(moveX, moveY);
        if (direccionActual != Vector2.zero)
            ultimaDireccion = direccionActual.normalized;
    }

    void Disparar()
    {
        GameObject proyectil = ProyectilPool.instancia.ObtenerProyectil();
        proyectil.transform.position = puntoDisparo.position;
        proyectil.transform.rotation = Quaternion.identity;
        proyectil.GetComponent<Proyectil>().Disparar(ultimaDireccion);
        if (sonidoDisparo != null)
            audioSource.PlayOneShot(sonidoDisparo);
    }
}