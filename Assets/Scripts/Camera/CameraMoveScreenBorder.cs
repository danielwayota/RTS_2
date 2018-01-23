/**
 * File: CameraMove
 * Author: Daniel Wayota
 * Author WebPage: http://danielwayota.tk
 * 
 * Description: Controla el movimiento de la cámara usando el mouse
 */
using UnityEngine;

public class CameraMoveScreenBorder : MonoBehaviour
{
    private int border;             // tamaño del borde de la pantalla donde se activa el movimiento.
    private Vector2 screenBorder;
    private Vector2 middleScreen;

    private Vector3 movement;
    // ============================
    void Start()
    {
        this.border = 5;

        this.screenBorder = new Vector2(Screen.width - this.border, Screen.height - this.border);
        this.middleScreen = new Vector2(Screen.width / 2, Screen.height / 2);
    }
    // ============================
    void Update()
    {
        // Siempre y cuando el mouse se esté moviendo intentamos mover la cámara.
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            Vector2 mouse = Input.mousePosition;
            if (
                (Input.mousePosition.x < this.border) ||
                (Input.mousePosition.x > (this.screenBorder.x)) ||
                (Input.mousePosition.y < this.border) ||
                (Input.mousePosition.y > (this.screenBorder.y))
            )
            {
                // Trazamos el vector de dirección entre la posicion del mouse
                // y el centro de la pantalla.
                this.movement = mouse - this.middleScreen;
                this.movement.z = this.movement.y;
                this.movement.y = 0;
                // Se convierte su módulo en 0.5.
                this.movement = this.movement.normalized / 2;

                this.transform.Translate(this.movement);
            }
        }
    }
}
