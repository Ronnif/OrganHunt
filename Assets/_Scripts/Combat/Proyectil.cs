using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float tiempoVida = 3f;
    [SerializeField] private float dano = 10f;

    private Rigidbody2D rb;
    private float temporizador;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        temporizador = 0f;
    }

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= tiempoVida)
        {
            Desactivar();
        }
    }

    public void Disparar(Vector2 direccion)
    {
        rb.linearVelocity = direccion.normalized * velocidad;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            other.GetComponent<EnemyHealth>().RecibirDano(dano);
            Desactivar();
        }
        else if (other.CompareTag("Pared"))
        {
            Desactivar();
        }
    }

    void Desactivar()
    {
        rb.linearVelocity = Vector2.zero;
        ProyectilPool.instancia.DevolverProyectil(gameObject);
    }
}