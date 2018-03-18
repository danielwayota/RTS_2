using UnityEngine;

public class CameraMoveKeyboard : MonoBehaviour
{
    public float speed = 2;

    public Transform corner1;
    public Transform corner2;

    private Bounds bounds;
    private Vector3 movement = Vector3.zero;


    void Start()
    {
        Vector3 center = Vector3.Lerp(corner1.position, corner2.position, 0.5f) ;
        Vector3 size = new Vector3(
            Mathf.Abs(corner1.position.x - corner2.position.x),
            Mathf.Abs(corner1.position.y - corner2.position.y),
            Mathf.Abs(corner1.position.z - corner2.position.z)
        );

        this.bounds = new Bounds(center, size);
    }

    // ================================
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            this.movement.Set(horizontal, 0, vertical);

            this.transform.position = this.bounds.ClosestPoint(
                this.transform.position + this.movement * this.speed * Time.deltaTime
            );
        }
    }

    // ================================
    void OnDrawGizmos()
    {
        if (corner1 != null && corner2 != null)
        {
            Vector3 center = Vector3.Lerp(corner1.position, corner2.position, 0.5f);
            Vector3 size = corner1.position - corner2.position;
            Gizmos.DrawWireCube(center, size);
        }
    }
}
