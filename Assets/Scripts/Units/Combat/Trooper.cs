using UnityEngine;

[RequireComponent(typeof(Walk))]
public class Trooper : MobileUnit
{
    [Header("Trooper")]

    private float time;
    private float timeOut = .25f;

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
        this.time += Time.deltaTime;

        // Reloj para verificar si hay enemigos cerca.
        //  Si no hay y la unidad está detenida,
        //  se genera una rotación aleatoria para simular "vigilancia"
        if (this.time > this.timeOut)
        {
            this.time = 0;

            this.currentTarget = this.GetVisibleEnemy();
        }

        // No hay enemigos cerca, estado de vigilancia.
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
