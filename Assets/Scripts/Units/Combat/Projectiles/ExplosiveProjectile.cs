using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [Header("Explosive")]
    public float explosionRadius = 4;

    public GameObject particles;

    public LayerMask unitsLayer;

    void OnCollisionEnter(Collision other)
	{
        Collider[] nearUnits = Physics.OverlapSphere(
            this.transform.position,
            this.explosionRadius,
            this.unitsLayer
        );

        for (int i = 0; i < nearUnits.Length; i++)
        {
            Unit u = nearUnits[i].gameObject.GetComponent<Unit>();

            if (u != null)
            {
                this.weapon.OnProjectileCollision(this, u);
            }
            else
            {
                Debug.LogError("Unit component not detected");
            }
        }

        // TODO: Create auto destroy component
        GameObject p = Instantiate(this.particles, this.transform.position, Quaternion.identity);
        Destroy(p, 5);
        Destroy(this.gameObject);
    }
}
