﻿using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int damage = 5;
    public float coolDownTime = 0.5f;

    [Header("World objects")]
    public Transform shootPoint;
    public GameObject projectilePrefab;

    [Header("Sound")]
    public SoundVariator shootAudio;


    private float time;

    private Unit unit;

    public bool isReady { get; private set; }

    void Start()
    {
        this.time = 0;
        this.isReady = true;

        this.unit = GetComponentInParent<Unit>();
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

    // =================================
    public void Shoot()
    {
        this.shootAudio.Play();
        this.isReady = false;

        GameObject p = Instantiate(
            this.projectilePrefab,
            this.shootPoint.position,
            this.shootPoint.rotation
        );

        p.GetComponent<Projectile>().weapon = this;
    }

    // =================================
    public void OnProjectileCollision(Projectile projectile, Unit otherUnit)
    {
        // Hacer daño a la otra unidad si es un enemigo.
        if (otherUnit.faction != this.unit.faction)
        {
            var health = otherUnit.GetComponent<Health>();

            health.Damage(this.damage, projectile.transform.forward * - 1);

            Destroy(projectile.gameObject);
        }
    }
}
