using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] public int danio = 1;

    private Transform player;//jugaror

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerStats.Instance.danioRecibido(danio);
            Debug.Log("Pinchos hizo "+ danio + "de danio");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerStats.Instance.danioRecibido(danio);
            Debug.Log("Pinchos hizo "+ danio + "de danio");
        }
    }
}
