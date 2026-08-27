using System;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //creamos un enum para guardar cada tipo de moneda, normal y la especial
    public enum CoinType
    {
        Normal,Special
    }
    //ponemos en que se vea en el inspector a la hora de ponerle el script elegir que tipo de moneda
    [SerializeField] private CoinType tipo = CoinType.Normal;
    [SerializeField] private int valor = 1;//valor de la moneda
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //buscamos el jugador con el tag
        if (collision.CompareTag("Player"))
        {
            //creamos la estancia de la clase PlayerStats
            PlayerStats player = collision.GetComponent<PlayerStats>();
            //si todo bien usamos el metodo que contiene para sumar monedas
            if(player != null)
            {
                //en un switch recogemos que tipo de moneda es, y dependiendo de cual ejecutamos
                //el codigo correspondiente.
                switch (tipo)
                {
                    case CoinType.Normal:
                        //sumamos la moneda
                        player.AddNomalCoin(valor);
                        //la destruimos.
                        Destroy(gameObject);
                        break;
                    case CoinType.Special:
                        player.AddSpecialCoin(valor);
                        Destroy(gameObject);
                        break;
                }
                
            }
        }
    }
}
