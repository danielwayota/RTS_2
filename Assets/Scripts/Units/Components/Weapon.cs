using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	[Header("Weapon Stats")]
    public int damage = 5;
    public float coolDownTime = 0.5f;

    [Header("World objects")]
    public Transform shootPoint;
    public GameObject projectilePrefab;


	private float time;

	private Unit unit;

	public bool isReady { get; private set; }

	void Start()
	{
		this.time = 0;
		this.isReady = true;

		this.unit = GetComponent<Unit>();		
	}

	// =================================
    void Update()
    {
        if (!this.isReady)
        {
            this.time += Time.deltaTime;

            if (this.time > this.coolDownTime)
            {
                this.isReady = true;
                this.time = 0;
            }
        }
    }

	public void Shoot()
	{
		this.isReady = false;

		GameObject p = Instantiate(
			this.projectilePrefab,
			this.shootPoint.position,
			this.shootPoint.rotation
		);

		p.GetComponent<Projectile>().weapon = this;
	}

	public void OnProjectileCollision(Projectile projectile, Unit unit)
	{
		if (unit.faction != this.unit.faction)
		{
			unit.GetComponent<Health>().health -= this.damage;
			Destroy(projectile.gameObject);
		}
	}
}
