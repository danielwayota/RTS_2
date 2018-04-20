using UnityEngine;
using UnityEngine.AI;

enum Status
{
    IDLE,
    MOVING
}

public class Trooper : Unit
{
    public LayerMask unitsLayer;
    public float maxCombatDistance = 10;

    private NavMeshAgent agent;

    private float time;
    private float timeOut = 1f;

    private Vector3 targetPosition;

    private Status status = Status.IDLE;

    private Weapon weapon;

    private Unit currentTarget;
    // ================================
    public override void Init()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.weapon = GetComponent<Weapon>();

        if (this.status != Status.IDLE)
        {
            this.agent.SetDestination(this.targetPosition);
        }
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

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

            if (this.status == Status.MOVING)
            {
                if (!this.agent.hasPath || this.agent.velocity.sqrMagnitude == 0f)
                {
                    // Movimiento terminado
                    this.status = Status.IDLE;
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

    public void CheckSurrounding()
    {
        // Check surroundings for enemies.
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
        this.targetPosition = worldPos;
        this.status = Status.MOVING;

        if (this.agent != null)
        {
            this.agent.SetDestination(worldPos);
        }
    }
}
