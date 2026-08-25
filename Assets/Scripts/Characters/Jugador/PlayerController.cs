using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpForce = 4;
    [SerializeField] public float groundRadius = 0.1f;
    [SerializeField] public float climbSpeed = 5f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float move;
    private float verticalImput;
    private bool isGrounded;
    private bool isClimbing = false;
    private bool isTocandoLadder = false;



    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        verticalImput = Input.GetAxisRaw("Vertical");

        animator.SetBool("isRunning", Mathf.Abs(rb2D.linearVelocityX) > 0.2);
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("VelocityY", rb2D.linearVelocity.y);
        animator.SetBool("isClimbing", isClimbing);




        ///Si el movimiento es menor que 0 gira hacia la izq
        /// Si el movimiento es mayor a 0 no gira se mantiene en la posicion, hacia la der
        /// 0 es iguala no hace nada se mantiene en la ultima direccion
        if (move < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (move > 0)
        {
            spriteRenderer.flipX = false;
        }

        Jump();

        // Si está tocando la escalera y pulsa arriba o abajo -> empieza a escalar
        if (isTocandoLadder && Mathf.Abs(verticalImput) > 0.1f)
        {
            isClimbing = true;
        }
    }



    void FixedUpdate()
    {


        if (isClimbing)
        {
            ///Mientras trepa quitamos la gravedad y nos movemos en vertical
            rb2D.gravityScale = 0f;
            rb2D.linearVelocity = new Vector2(move * speed, verticalImput * climbSpeed);
        }
        else
        {
            rb2D.gravityScale = 1f;
            rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);
            ///comprueba constantemente si esta colisionando con la layer de Ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        }
    }



    private void Jump()
    {
        // Salto normal: Solo si presionas el botón Y estás en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }
    }

    // --- Detección de la escalera ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isTocandoLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isTocandoLadder = false;
            isClimbing = false;
            rb2D.gravityScale = 1f; // recuperamos gravedad al salir
        }
    }
}