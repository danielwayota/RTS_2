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
    private float timeOut = 1f;

    private Unit currentTarget;

    // ================================
    void Awake()
    {
        // TODO: Mover el arma en sí a un hijo de la unidad
        this.weapon = GetComponent<Weapon>();
        this.walk = GetComponent<Walk>();

        this.animator = GetComponent<Animator>();

        this.animator.SetBool("Locked", false);
        this.isActive = false;
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

                if (this.currentTarget == null)
                    return;

                Vector3 target = this.currentTarget.transform.position;

                // TODO: Rotar unicamente la "cabeza" de la torreta
                this.transform.LookAt(target);

                if (this.weapon.isReady)
                {
                    this.weapon.Shoot();
                }
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
