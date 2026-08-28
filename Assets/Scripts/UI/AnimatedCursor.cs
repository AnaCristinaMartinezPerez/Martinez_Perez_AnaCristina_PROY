using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
    public Texture2D cursorTexture;   // Arrastra aquí tu PNG
    public Vector2 hotspot = Vector2.zero; // Punto de clic (0,0 = esquina superior izquierda)
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }
}
