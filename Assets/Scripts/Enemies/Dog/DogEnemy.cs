using UnityEngine;

public class DogEnemy : MonoBehaviour
{

    [SerializeField] public float speed = 3f;//velocidad de movimiento
    [SerializeField] public float detectionRadius = 6.0f;//el radio de deteccion del jugador
    [SerializeField] public int danio = 1;
    private Transform player;//jugaror
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private SpriteRenderer spriteRenderer;   // Para hacer flip
    private Animator animator;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //metemos en GameObjet el objeto con el tag Player
        GameObject objetoJugador = GameObject.FindGameObjectWithTag("Player");

        if (objetoJugador != null)
        {
            player = objetoJugador.transform;
        }
        else
        {
            Debug.LogError("No se encontró el Player");
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
             //metemos en una variable de tipo float un numero que sera determinado por el metodo
            //Distance al que le tenemos que meter dos parametros, estos parametros seran
            //la posicion del enemigo y la posicion del jugador
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRadius)
            {
                
                //entonces metemos en un objeto tipo Vector2 la posicion del jugador menos la posicion del enemigo
                //esta sera la direccion que tomara el enemigo cuando lo encuente
                Vector2 direction = (player.position - transform.position).normalized;
                //
                movementDirection = new Vector2(direction.x, 0).normalized;

                //para el flip del enemigo
                if (movementDirection.x > 0)
                {
                    spriteRenderer.flipX = true;   // Mira a la derecha
                }
                else if (movementDirection.x < 0)
                {
                    spriteRenderer.flipX = false;    // Mira a la izquierda
                }
            }
            else
            {
                //en caso de que se salga del radio, el enemigo dejara de moverse
                movementDirection = Vector2.zero;
            }
            //animaciones----------------------------------------------------------------
            float currentSpeed = Mathf.Abs(movementDirection.x * speed);
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //obtenemos su rigibody.linearVelocity que sera igual a la direccion en la que debe moverse por la velocidad
            //determinada
            rb.linearVelocity = new Vector2(movementDirection.x * speed, rb.linearVelocity.y);
        }
    }

    //trigger que se activa cuando toca la el collider de la cabeza del enemigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (collision.CompareTag("Player"))
            {
                // Comprobamos que el jugador esta cayendo (para que solo cuente si lo pisa desde arriba)
                Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

                if (playerRb != null && playerRb.linearVelocityY < 0)   // Esta cayendo
                {
                    Morir();//metodo para indicar que el enemigo ha muerto y todo lo necesario
                    // Rebote del jugador 
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocityX, 5f);
                }
            }
        }
    }

        //colision normal de tocar el cuerpo del enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //llamamos al singleton y el pasamos el danio de este enemigo
                PlayerStats.Instance.danioRecibido(danio);

                Debug.Log("Perro hizo "+ danio + "de danio");
            }
        }
    }

        public void Morir()
    {
        isDead = true;//pasa a verdadero
        movementDirection = Vector2.zero;//el bicho ya no se mueve
        rb.linearVelocity = Vector2.zero;

        //ponemos el tigger llamado die
        animator.SetTrigger("Die"); 
        //destruimos el enemigo despues de la animacion de muerte
        //para ajustarla mas o menos bien, debemos ver cuanto tiempo tiene
        //dicha animacion
        Destroy(gameObject, 1.5f);
        
    }
}
