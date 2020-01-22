using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Walk))]
public class Trooper : Unit
{
    public LayerMask unitsLayer;
    public float maxCombatDistance = 10;
    public float maxViewDistance = 20;
    public float halfFieldOfView = 15;

    private float time;
    private float timeOut = .25f;

    private Weapon weapon;
    private Walk walk;

    private Unit currentTarget;

    private Quaternion vigilanceRotation;

    // ================================
    void Awake()
    {
        this.weapon = GetComponent<Weapon>();
        this.walk = GetComponent<Walk>();

        this.vigilanceRotation = this.transform.rotation;
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

                if (this.walk.status == WalkStatus.IDLE)
                {
                    this.vigilanceRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                }
            }
            this.CheckSurrounding();
        }


        if (this.currentTarget == null)
        {
            if (this.walk.status == WalkStatus.IDLE)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.vigilanceRotation, Time.deltaTime);
            }
        }
        else
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
        this.currentTarget = null;

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

        if (this.currentTarget != null)
        {
            // Hay una cerca
            return;
        }

        nearUnits = Physics.OverlapSphere(
            this.transform.position,
            this.maxViewDistance,
            this.unitsLayer
        );

        for (int i = 0; i < nearUnits.Length; i++)
        {
            if (nearUnits[i].gameObject != this.gameObject)
            {
                Unit posibleEnemy = nearUnits[i].GetComponent<Unit>();

                if (posibleEnemy.faction != this.faction)
                {
                    Vector3 toEnemy = (posibleEnemy.transform.position - this.transform.position).normalized;

                    float angle = Mathf.Abs(Vector3.Angle(this.transform.forward, toEnemy));

                    if (angle < this.halfFieldOfView)
                    {
                        this.currentTarget = posibleEnemy;
                    }
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
