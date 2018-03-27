using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	public float speed = 8;

	public Weapon weapon;

	public float duration = 10;

	void Start()
	{
		Destroy(this.gameObject, this.duration);

		GetComponent<Rigidbody>().velocity = this.transform.forward * this.speed;
	}


	void OnCollisionEnter(Collision other)
	{
		Unit u = other.gameObject.GetComponent<Unit>();

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
