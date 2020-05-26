using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public abstract class Faction : MonoBehaviour
{
    public float maxEnergy = 100000;

    public Material materialColor;

    public string spawnPointTag;

    public GameObject basePrefab;

    private UnitManager _unitManager;
    [HideInInspector]
    public UnitManager unitManager
    {
        get
        {
            if (this._unitManager == null)
            {
                this._unitManager = GetComponent<UnitManager>();
            }

            return this._unitManager;
        }
    }

    protected float energy;

    private List<Alert> alerts;

    // ======================================
    protected virtual void Awake()
    {
        this.energy = 1000;

        GameObject[] spawns = GameObject.FindGameObjectsWithTag(this.spawnPointTag);
        int index = Random.Range(0, spawns.Length);

        var targetSpawnPoint = spawns[index];

        var theBaseGO = Instantiate(
            this.basePrefab,
            targetSpawnPoint.transform.position,
            targetSpawnPoint.transform.rotation
        );

        var theBase = theBaseGO.GetComponent<Base>();

        theBase.faction = this;

        this.alerts = new List<Alert>();
    }

    // ======================================
    void Update()
    {
        int length = this.alerts.Count;
        for (int i = length - 1; i >= 0 ; i--)
        {
            var alert = this.alerts[i];
            alert.time -= Time.deltaTime;

            if (alert.time <= 0)
            {
                this.alerts.RemoveAt(i);
            }
        }
    }

    // ======================================
    public bool CanRetrieveEnergy(float amount)
    {
        return this.energy >= amount;
    }

    // ======================================
    public virtual float RetrieveEnergy(float amount)
    {
        if (amount < 0) { Debug.LogError("Trying to retrieve negative energy!"); return 0; }

        if (this.energy >= amount)
        {
            this.energy -= amount;
        }
        else
        {
            amount = this.energy;
            this.energy = 0;
        }

        return amount;
    }

    // ======================================
    public virtual float StoreEnergy(float amount)
    {
        if (amount < 0) { Debug.LogError("Trying to store negative energy!"); return 0; }

        float spaceForEnergy = this.maxEnergy - this.energy;

        if (amount > spaceForEnergy)
        {
            this.energy += spaceForEnergy;

            amount = spaceForEnergy;
        }
        else
        {
            this.energy += amount;
        }

        return amount;
    }

    public void PushAlert(Vector3 position)
    {
        var tmp = new Alert(position, 1f);
        this.alerts.Add(tmp);
    }

    public Alert PullNearAlert(Vector3 position, float maxDistance)
    {
        if (this.alerts.Count == 0)
            return null;

        Alert nearest = null;
        float nearestSqDistance = 0;

        float sqMaxDistance = Mathf.Pow(maxDistance, 2);

        int length = this.alerts.Count;
        for (int i = length - 1; i >= 0 ; i--)
        {
            var alert = this.alerts[i];

            float sqDistance = (alert.position - position).sqrMagnitude;

            if (sqDistance > sqMaxDistance)
            {
                continue;
            }

            if (nearest == null || (sqDistance < nearestSqDistance))
            {
                nearest = alert;
                nearestSqDistance = sqDistance;
            }
        }

        if (nearest != null)
        {
            this.alerts.Remove(nearest);
        }

        return nearest;
    }
}
