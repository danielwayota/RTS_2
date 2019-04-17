using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Walk))]
public class Trooper : Unit
{
    public LayerMask unitsLayer;
    public float maxCombatDistance = 10;

    private float time;
    private float timeOut = 1f;

    private Weapon weapon;
    private Walk walk;

    private Unit currentTarget;

    // ================================
    void Awake()
    {
        this.weapon = GetComponent<Weapon>();
        this.walk = GetComponent<Walk>();
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

            // Combate
            if (this.currentTarget == null)
            {
                this.CheckSurrounding();
            }
            else
            {
                float distance = Vector3.Distance(
                    this.transform.position, this.currentTarget.transform.position
                );

                if (distance > this.maxCombatDistance)
                {
                    this.currentTarget = null;
                }
            }
        }


        if (this.currentTarget != null)
        {
            Vector3 target = this.currentTarget.transform.position;
            this.transform.LookAt(target);

            if (this.weapon.isReady)
            {
                this.weapon.Shoot();
            }
        }
    }

    // =================================
    public void CheckSurrounding()
    {
        // Comprobar los alrededores.
        Collider[] nearUnits = Physics.OverlapSphere(
            this.transform.position,
            this.maxCombatDistance - 1,
            this.unitsLayer
        );

        for (int i = 0; i < nearUnits.Length; i++)
        {
            if (nearUnits[i].gameObject != this.gameObject)
            {
                Unit posibleEnemy = nearUnits[i].GetComponent<Unit>();

                if (posibleEnemy.faction != this.faction)
                {
                    this.currentTarget = posibleEnemy;
                }
            }
        }
    }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.walk.SetDestination(worldPos);
    }
}
