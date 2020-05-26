using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 8;

    public Weapon weapon;

    // ================================
    void Start()
    {
        GetComponent<Rigidbody>().velocity = this.transform.forward * this.speed;
    }

    // ================================
    void OnTriggerEnter(Collider other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();

        // Si el objecto con el que chocamos es una unidad
        //   avisar al arma.
        // Si es cualquier otra cosa, destruir el proyectil.
        if (u != null)
        {
            this.weapon.OnProjectileCollision(this, u);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
