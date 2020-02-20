using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float time = 1f;

    void Start()
    {
        Destroy(this.gameObject, this.time);
    }
}