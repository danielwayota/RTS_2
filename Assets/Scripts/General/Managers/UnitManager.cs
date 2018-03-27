using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    [Header("Units")]
    public List<Unit> units;

    [HideInInspector]
    public Faction faction;

    void Awake()
    {
        this.faction = GetComponent<Faction>();
    }

    public virtual void RemoveUnit(Unit u)
    {
        this.units.Remove(u);
    }
}
