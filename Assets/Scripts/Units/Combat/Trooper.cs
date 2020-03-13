using UnityEngine;

[RequireComponent(typeof(Walk))]
public class Trooper : Unit
{
    [Header("Trooper")]
    public LayerMask unitsLayer;
    public float detectionDistance = 10;
    public float maxViewDistance = 20;
    public float halfFieldOfView = 15;

    private float time;
    private float timeOut = .25f;

    private Weapon weapon;
    private Walk walk;

    private Sensor sensor;

    private Unit currentTarget;

    // private Quaternion vigilanceRotation;

    // ================================
    void Awake()
    {
        this.weapon = this.RequireComponent<Weapon>();
        this.walk = this.RequireComponent<Walk>();

        this.sensor = this.RequireComponent<Sensor>();
        this.sensor.caller = this;

        // this.vigilanceRotation = this.transform.rotation;
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj para verificar si hay enemigos cerca.
        //  Si no hay y la unidad está detenida,
        //  se genera una rotación aleatoria para simular "vigilancia"
        if (this.time > this.timeOut)
        {
            this.time = 0;

            // if (this.currentTarget == null)
            // {
            //     if (this.walk.status == WalkStatus.IDLE)
            //     {
            //         this.vigilanceRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            //     }
            // }
            this.currentTarget = this.GetVisibleEnemy();
        }

        // No hay enemigos cerca, estado de vigilancia.
        if (this.currentTarget == null)
        {
            // if (this.walk.status == WalkStatus.IDLE)
            // {
            //     this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.vigilanceRotation, Time.deltaTime);
            // }
        }
        // Enemigos en la vista, estado de combate.
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
    }
}
