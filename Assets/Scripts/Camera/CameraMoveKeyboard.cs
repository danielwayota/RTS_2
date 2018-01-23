using UnityEngine;

public class CameraMoveKeyboard : MonoBehaviour
{
    public float speed = 2;

    private Vector3 movement = Vector3.zero;

    // ================================
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            this.movement.Set(horizontal, 0, vertical);

            this.transform.Translate(this.movement * this.speed * Time.deltaTime);
        }
    }
}
