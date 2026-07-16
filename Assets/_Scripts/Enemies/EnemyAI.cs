using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum EstadoEnemigo { Patrullar, Perseguir, Atacar }
    private EstadoEnemigo estadoActual = EstadoEnemigo.Patrullar;

    [Header("Referencias")]
    [SerializeField] private Transform jugador;

    [Header("Detección")]
    [SerializeField] private float rangoDeteccion = 5f;
    [SerializeField] private float rangoAtaque = 1f;

    [Header("Movimiento")]
    [SerializeField] private float velocidadPatrulla = 1f;
    [SerializeField] private float velocidadPersecucion = 2.5f;
    [SerializeField] private float radioPatrulla = 3f;

    [Header("Ataque")]
    [SerializeField] private float danoAtaque = 10f;
    [SerializeField] private float cadenciaAtaque = 1f;

    private Rigidbody2D rb;
    private Vector2 puntoInicial;
    private Vector2 destinoPatrulla;
    private float tiempoUltimoAtaque;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        puntoInicial = transform.position;
        ElegirNuevoPuntoPatrulla();

        if (jugador == null)
        {
            GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
            if (jugadorObj != null)
                jugador = jugadorObj.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        // Transiciones de estado
        if (distanciaJugador <= rangoAtaque)
        {
            estadoActual = EstadoEnemigo.Atacar;
        }
        else if (distanciaJugador <= rangoDeteccion)
        {
            estadoActual = EstadoEnemigo.Perseguir;
        }
        else
        {
            estadoActual = EstadoEnemigo.Patrullar;
        }

        // Ejecutar comportamiento según el estado
        switch (estadoActual)
        {
            case EstadoEnemigo.Patrullar:
                Patrullar();
                break;
            case EstadoEnemigo.Perseguir:
                Perseguir();
                break;
            case EstadoEnemigo.Atacar:
                Atacar();
                break;
        }
    }

    void Patrullar()
    {
        Vector2 direccion = (destinoPatrulla - (Vector2)transform.position).normalized;
        rb.linearVelocity = direccion * velocidadPatrulla;

        if (Vector2.Distance(transform.position, destinoPatrulla) < 0.2f)
        {
            ElegirNuevoPuntoPatrulla();
        }
    }

    void Perseguir()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.linearVelocity = direccion * velocidadPersecucion;
    }

    void Atacar()
    {
        rb.linearVelocity = Vector2.zero;

        if (Time.time >= tiempoUltimoAtaque + cadenciaAtaque)
        {
            PlayerHealth vidaJugador = jugador.GetComponent<PlayerHealth>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDano(danoAtaque);
            }
            tiempoUltimoAtaque = Time.time;
        }
    }

    void ElegirNuevoPuntoPatrulla()
    {
        Vector2 direccionAleatoria = Random.insideUnitCircle * radioPatrulla;
        destinoPatrulla = puntoInicial + direccionAleatoria;
    }
}