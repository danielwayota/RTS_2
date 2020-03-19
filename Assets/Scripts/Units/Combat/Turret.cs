using UnityEngine;

public class Turret : MobileUnit
{
    [Header("Turret")]
    private Weapon weapon;

    private Animator animator;
    private bool isActive;

    private float time;
    private float timeOut = .5f;

    private Sensor sensor;

    private Unit currentTarget;

    /// ====================================
    void Awake()
    {
        this.weapon = this.RequireComponent<Weapon>();

        this.animator = this.RequireComponent<Animator>();

        this.animator.SetBool("Locked", false);
        this.isActive = false;

        this.sensor = this.RequireComponent<Sensor>();
        this.sensor.caller = this;
    }

    /// ====================================
    protected override void Update()
    {
        base.Update();

        if (!this.isActive)
            return;

        this.time += Time.deltaTime;
        if (this.time >= this.timeOut)
        {
            this.time = 0;

            this.currentTarget = this.GetVisibleEnemy();
        }

        if (this.currentTarget == null)
            return;

        Vector3 target = this.currentTarget.transform.position;

        this.weapon.transform.LookAt(target);

        if (this.weapon.isReady)
        {
            this.weapon.Shoot();
        }
    }

    /// ====================================
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

    /// ====================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="status"></param>
    protected override void OnWalkStatusUpdated(WalkStatus status)
    {
        switch (status)
        {
            case WalkStatus.IDLE:
                this.animator.SetBool("Locked", true);
                this.isActive = true;
                break;
            case WalkStatus.MOVING:
                this.animator.SetBool("Locked", false);
                this.isActive = false;
                break;
        }
    }
}
