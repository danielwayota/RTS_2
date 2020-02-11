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

    private Unit currentTarget;

    // private Quaternion vigilanceRotation;

    // ================================
    void Awake()
    {
        this.weapon = GetComponent<Weapon>();
        this.walk = GetComponent<Walk>();

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

        if (nextTarget != null)
        {
            // Hay una cerca, no hay necesidad de comprobar el arco de visión
            return nextTarget;
        }

        // Comprobar el arco de visión
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

                    // ¿El enemigo está dentro del arco de visión?
                    if (angle < this.halfFieldOfView)
                    {
                        nextTarget = posibleEnemy;
                    }
                }
            }
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
