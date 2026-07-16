using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 5f;

    [Header("Habilidad Correr (Pulmón)")]
    [SerializeField] private bool tieneHabilidadCorrer = false;
    [SerializeField] private float multiplicadorCorrer = 1.8f;
    [SerializeField] private float duracionCorrer = 3f;
    [SerializeField] private float cooldownCorrer = 5f;

    private float tiempoCorrerRestante;
    private float tiempoCooldownRestante;
    private bool estaCorriendo = false;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movimiento;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tiempoCorrerRestante = duracionCorrer;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;

        float moveX = (kb.dKey.isPressed || kb.rightArrowKey.isPressed ? 1f : 0f)
                    - (kb.aKey.isPressed || kb.leftArrowKey.isPressed ? 1f : 0f);

        float moveY = (kb.wKey.isPressed || kb.upArrowKey.isPressed ? 1f : 0f)
                    - (kb.sKey.isPressed || kb.downArrowKey.isPressed ? 1f : 0f);

        movimiento = new Vector2(moveX, moveY).normalized;

        bool isMoving = movimiento.magnitude > 0;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("MoveX", Mathf.Abs(moveX));
            animator.SetFloat("MoveY", moveY);
            spriteRenderer.flipX = moveX > 0;
        }

        ManejarSprint(kb);
    }

    void ManejarSprint(Keyboard kb)
    {
        if (!tieneHabilidadCorrer) return;

        if (kb.spaceKey.isPressed && tiempoCorrerRestante > 0f && !estaCorriendo)
        {
            estaCorriendo = true;
        }

        if (estaCorriendo)
        {
            if (kb.spaceKey.isPressed && tiempoCorrerRestante > 0f)
            {
                tiempoCorrerRestante -= Time.deltaTime;
            }
            else
            {
                estaCorriendo = false;
                tiempoCooldownRestante = cooldownCorrer;
            }
        }
        else if (tiempoCorrerRestante < duracionCorrer)
        {
            tiempoCooldownRestante -= Time.deltaTime;
            if (tiempoCooldownRestante <= 0f)
            {
                tiempoCorrerRestante = duracionCorrer;
            }
        }
    }

    void FixedUpdate()
    {
        float velocidadFinal = estaCorriendo ? velocidad * multiplicadorCorrer : velocidad;
        rb.linearVelocity = movimiento * velocidadFinal;
    }

    public void ActivarHabilidadCorrer()
    {
        tieneHabilidadCorrer = true;
    }
}