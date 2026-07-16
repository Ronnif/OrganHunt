using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 30f;
    private float vidaActual;
    private Animator animator;
    private bool estaMuerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
    }

    public void RecibirDano(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaMuerto = true;
        animator.SetTrigger("Morir");

        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        GameManager.instancia.RegistrarEnemigoDerrotado();

        Destroy(gameObject, 1f);
    }
}