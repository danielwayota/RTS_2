using UnityEngine;

public class Alert
{
    public Vector3 position;
    public Vector3 origin;
    public float time;

    public Alert(Vector3 position, Vector3 origin, float time)
    {
        this.position = position;
        this.origin = origin;
        this.time = time;
    }
}
