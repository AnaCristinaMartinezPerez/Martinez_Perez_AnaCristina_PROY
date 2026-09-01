using UnityEngine;
using UnityEngine.UI;

public class LogicaFullScreen : MonoBehaviour
{
    public Toggle casilla;//para la casilla marcada
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //al inicio, va a detectar si esta en pantalla completa o en modo ventan
        //si la pantalla está en modo pantalla completa, implica que la casilla
        //está marcada, en caso de que no, no lo está
        if (Screen.fullScreen)
        {
            casilla.isOn = true;
        }else
        {
            casilla.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo que define que si está marcado está en pantalla completa
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
