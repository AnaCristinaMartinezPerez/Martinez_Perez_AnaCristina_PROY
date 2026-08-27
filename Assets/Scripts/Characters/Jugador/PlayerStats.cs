using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Variable estatica que guarda la unica instancia (Singleton)
    public static PlayerStats Instance;

    public int maxVida = 5;
    public int vidaActual;

    public int totalNomalCoins = 0;
    public int totalSpecialCoins = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //No destruir al cambiar de escena, para que se mantenga entre los distintos niveles.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//si ya existe uno lo destruimos
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //al comenzar la partida ponemos la vida al maximo
        vidaActual = maxVida;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void danioRecibido(int danio)
    {
        vidaActual -= danio;

        //evitamos que la vida baje de 0, controlar negativos
        if(vidaActual < 0)
        {
            vidaActual = 0;
            Debug.Log("Vida actual: " + vidaActual);
        }
        if(vidaActual <= 0)
        {
            Muere();
        }
    }

    //metodo para la curasion
    public void Curarse(int cantidad)
    {
        vidaActual += cantidad;
        if(vidaActual > maxVida)
        {
            vidaActual = maxVida;
        }
    }

    //metodo para decir que ha muerto el personaje
    private void Muere()
    {
        Debug.Log("El jugador ha muerto");
    }

    public void AddNomalCoin(int valor)
    {
        totalNomalCoins += valor;
        Debug.Log("Monedas: " + totalNomalCoins);
        //aqui actualizamos ui ejemplo updatecoinui + llamada al manager sonido
    }

    public void AddSpecialCoin(int valor)
    {
        totalSpecialCoins += valor;
        Debug.Log("Monedas especiales: " + totalSpecialCoins);
    }

}
