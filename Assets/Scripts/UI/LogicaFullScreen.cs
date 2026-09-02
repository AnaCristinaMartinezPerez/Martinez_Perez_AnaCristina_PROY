using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaFullScreen : MonoBehaviour
{
    public Toggle casilla;//para la casilla marcada
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;//array de tipo resolucion, que recoge todas las resoluciones que soporta el ordenador
    //lista de string para meter cada opcion de resolucion disponible
    private List<string> opciones = new List<string>();
    //entero para decidir la resolucion por la posicion en la lista
    private int resolucionActual = 0;
    private string opcion;
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

        ComprobarResolucion();
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

    public void ComprobarResolucion()
    {
        //guardamos en el array de Resolutions, todas las resoluciones que permite el ordenador
        resoluciones = Screen.resolutions;
        //limpiamos el desplegable
        resolucionesDropDown.ClearOptions();
        
        //con un for recorremos, el array con todas las resoluciones que soporta el ordenador
        //si soporta 10 lo recorrera 10 veces, depende como se llene el array
        for (int i = 0; i < resoluciones.Length; i++)
        {
            //en el atributo opcion metemos la resolucion formateada, que seria el ancho x el alto ej: 1920 x 1080
            opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            //y se lo añadimos a la lista
            opciones.Add(opcion);
            //si la opcion que acabamos de guardar en la lista es igual a la que tenemos en el juego,
            //si es así guardamos la resolucion actual de nuestra pantalla en resolucionAcrual
            //y así ya sabemos en que posicion está
            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
            resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        //le añadimos la lista de opciones a la lista
        resolucionesDropDown.AddOptions(opciones);
        //con esto definimos la resolucion indicada en la lista por posicion: 0,1,2...
        resolucionesDropDown.value = resolucionActual;
        //actualiza la lista guardada
        resolucionesDropDown.RefreshShownValue();
        //guardamos el valor en posicionResolucion
        resolucionesDropDown.value = PlayerPrefs.GetInt("posicionResolucion",0);
    }
    /**
    Metodo encargado de cambiar la resolucion desde el desplegable, por el indice
    **/
    public void CambiarResolucion(int indiceListaResolucion)
    {
        //una vez cambiado la resolucion se guarda para cuando se cierra el juego
        PlayerPrefs.SetInt("posicionResolucion",resolucionesDropDown.value);

        //guardamos la resolucion seleccionada en el despegable
        Resolution resolucion = resoluciones[indiceListaResolucion];
        //cambiamos la resolucion
        //primer valor ancho, segundo algo, tercero booleano que indica que está en pantalla completa o no 
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
}
