using UnityEngine;

public class Turret : Unit
{
    [Header("Turret")]
    public float detectionDistance;
    public LayerMask unitsLayer;

    private Weapon weapon;
    private Walk walk;

    private Animator animator;
    private bool isActive;

    private float time;
    private float timeOut = .5f;

    private Sensor sensor;

    private Unit currentTarget;

    // ================================
    void Awake()
    {
        this.weapon = this.RequireComponent<Weapon>();
        this.walk = this.RequireComponent<Walk>();

        this.animator = this.RequireComponent<Animator>();

        this.animator.SetBool("Locked", false);
        this.isActive = false;

        this.sensor = this.RequireComponent<Sensor>();
        this.sensor.caller = this;
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        if (this.time >= this.timeOut)
        {
            this.time = 0;

            // La torreta está estática, toca buscar enemigos
            if (this.walk.status == WalkStatus.IDLE)
            {
                if (this.isActive == false)
                {
                    this.isActive = true;
                    this.animator.SetBool("Locked", true);
                }

                this.currentTarget = this.GetVisibleEnemy();

            }
        }

        if (!this.isActive)
            return;

        if (this.currentTarget == null)
            return;

        Vector3 target = this.currentTarget.transform.position;

        this.weapon.transform.LookAt(target);

        if (this.weapon.isReady)
        {
            this.weapon.Shoot();
        }
    }

    /// =================================
    /// <summary>
    /// Comprueba si hay enemigos cerca.
    /// </summary>
    public Unit GetVisibleEnemy()
    {
        Unit nextTarget = null;

        var posibleTargets = this.sensor.GetSensedUnits();

        foreach (var target in posibleTargets)
        {
            if (target.faction == this.faction)
                continue;

            nextTarget = target;
        }

        return nextTarget;
    }

    /// ========================================
    /// EXECUTE ORDER 66
    /// ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.walk.SetDestination(worldPos);
        this.animator.SetBool("Locked", false);
        this.isActive = false;
    }
}
