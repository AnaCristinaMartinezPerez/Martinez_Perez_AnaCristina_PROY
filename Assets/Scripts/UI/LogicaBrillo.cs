using UnityEngine;
using UnityEngine.UI;//interfaz de usuario

public class LogicaBrillo : MonoBehaviour
{

    public Slider slider;//para recoger el slider
    public float sliderValue;//para recoger el valor del slider
    public Image panelBrillo;//para modifical el alpha del panel negro para el brillo
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //por defecto
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        //con esto recogemos el el valor alpha del panel, los primeros tres valores, se mantienen que son red green y blue
        //y el ultimo es el que vamos a ir cambiando.
        panelBrillo.color = new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b,slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSlider(float valor)
    {
        //cambiamos el valor del slider
        sliderValue = valor;
        //guardamos el nuevo valor para que se mantenga a la hora de cerrar el juego
        PlayerPrefs.SetFloat("brillo",sliderValue);
        //
        panelBrillo.color = new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b,sliderValue);
    }
}
