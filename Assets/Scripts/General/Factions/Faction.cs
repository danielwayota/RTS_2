using UnityEngine;

public class Faction : MonoBehaviour
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

    // ======================================
    protected virtual void Awake()
    {
        this.energy = 1000;
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
}
