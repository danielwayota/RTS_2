using UnityEngine;

[RequireComponent(typeof(Walk))]
public class Trooper : MobileUnit
{
    [Header("Trooper")]

    private float visionTime;
    private float visionTimeOut = .25f;

    private float alertTime;
    private float alertTimeOut = .5f;

    private Weapon weapon;

    private Sensor sensor;
    private Unit currentTarget;

    // ================================
    public override void Init()
    {
        base.Init();
        this.weapon = this.RequireComponent<Weapon>();

        this.sensor = this.RequireComponent<Sensor>();
        this.sensor.caller = this;
    }

    // ================================
    protected override void Update()
    {
        base.Update();
        this.visionTime += Time.deltaTime;
        this.alertTime += Time.deltaTime;

        // Reloj para verificar si hay enemigos cerca.
        //  Si no hay y la unidad está detenida,
        //  se genera una rotación aleatoria para simular "vigilancia"
        if (this.visionTime > this.visionTimeOut)
        {
            this.visionTime = 0;

            this.currentTarget = this.GetVisibleEnemy();
        }

        if (this.alertTime > this.alertTimeOut)
        {
            this.alertTime = 0;

            if (this.currentTarget != null)
            {
                this.faction.PushAlert(this.currentTarget.transform.position);
            }
            else
            {
                Alert alert = this.faction.PullNearAlert(
                    this.transform.position,
                    this.sensor.maxViewDistance
                );

                if (alert != null)
                {
                    this.transform.LookAt(alert.position);
                }
            }
        }

        // Enemigo a la vista
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
}
