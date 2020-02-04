using UnityEngine;

public class Turret: Unit
{
    public float detectionDistance = 1;
    public LayerMask unitsLayer;

    private float time;
    private float timeOut = 1f;

    private Animator animator;

    private Weapon weapon;
    private Walk walk;

    private Unit currentTarget;

    private bool isActive;

    // ================================
    private void Awake()
    {
        this.animator = GetComponent<Animator>();

        this.weapon = GetComponent<Weapon>();
        this.walk = GetComponent<Walk>();

        this.isActive = false;
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

            if (this.walk.status == WalkStatus.IDLE && !this.isActive)
            {
                // Turn on the turret
                this.isActive = true;
            }

            if (this.isActive)
            {
                // Search targets and shoot
                this.currentTarget = this.GetVisibleEnemy();
            }
        }

        if (this.currentTarget != null && this.isActive)
        {
            Vector3 target = this.currentTarget.transform.position;
            this.transform.LookAt(target);

            if (this.weapon.isReady)
            {
                this.weapon.Shoot();
            }
        }
    }

    /// =================================
    /// <summary>
    /// Comprueba si hay enemigos cerca.
    /// </summary>
    public Unit GetVisibleEnemy()
    {
        Unit nextTarget = null;

        // Comprobar los alrededores.
        Collider[] nearUnits = Physics.OverlapSphere(
            this.transform.position,
            this.detectionDistance,
            this.unitsLayer
        );

        for (int i = 0; i < nearUnits.Length; i++)
        {
            if (nearUnits[i].gameObject != this.gameObject)
            {
                Unit posibleEnemy = nearUnits[i].GetComponent<Unit>();

                if (posibleEnemy.faction != this.faction)
                {
                    nextTarget = posibleEnemy;
                }
            }
        }

        return nextTarget;
    }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.walk.SetDestination(worldPos);
        // Turn off the turret
        this.isActive = false;
        this.currentTarget = null;
    }
}
