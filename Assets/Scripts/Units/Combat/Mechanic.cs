using UnityEngine;

public class Mechanic : Unit
{
    [Header("Mechanic")]
    public GameObject healingEffect;

    public float detectionDistance = 1f;
    public LayerMask unitsLayer;

    public int healingPoints = 5;

    private float time;
    private float timeOut = 1f;

    private Walk walk;

    // ================================
    void Awake()
    {
        this.walk = GetComponent<Walk>();
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

            // Check near friend units

            var unitHealth = this.GetNearFriendToRepair();

            if (unitHealth != null)
            {
                unitHealth.health += this.healingPoints;

                // TODO: Create auto destroy component
                GameObject effect = Instantiate(this.healingEffect, unitHealth.transform.position, Quaternion.identity);
            }
        }
    }

    /// =================================
    /// <summary>
    ///
    /// </summary>
    public Health GetNearFriendToRepair()
    {
        // Comprobar los alrededores.
        Collider[] nearUnits = Physics.OverlapSphere(
            this.transform.position,
            this.detectionDistance,
            this.unitsLayer
        );

        float minHealthFound = float.MaxValue;
        Health nearFriendUnit = null;

        // Busca el compañero con el menor nivel de salud.
        for (int i = 0; i < nearUnits.Length; i++)
        {
            if (nearUnits[i].gameObject != this.gameObject)
            {
                Unit posibleFriend = nearUnits[i].GetComponent<Unit>();

                if (posibleFriend.faction != this.faction)
                    continue;

                var healthComp = posibleFriend.GetComponent<Health>();
                if (!healthComp.isDamaged)
                    continue;

                if (healthComp.health < minHealthFound) {
                    minHealthFound = healthComp.health;

                    nearFriendUnit = healthComp;
                }
            }
        }

        return nearFriendUnit;
    }

    /// ========================================
    /// EXECUTE ORDER 66
    /// ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.walk.SetDestination(worldPos);
    }
}
