using UnityEngine;
using System.Collections.Generic;

public class AISquad
{
    public AISquadType type;
    public List<Unit> units;

    protected int targetCount;

    /// ====================================================
    public bool isEmpty
    {
        get
        {
            int length = this.units.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (this.units[i] == null)
                {
                    this.units.RemoveAt(i);
                }
            }

            return this.units.Count == 0;
        }
    }

    /// ====================================================
    public bool isComplete
    {
        get => this.units.Count == this.targetCount;
    }

    /// ====================================================
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="targetCount"></param>
    public AISquad(AISquadType type, int targetCount)
    {
        this.type = type;
        this.units = new List<Unit>();

        this.targetCount = targetCount;
    }

    /// ====================================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="target"></param>
    public void ExecuteOrder(Vector3 target)
    {
        foreach (var unit in this.units)
        {
            unit.ExecuteOrder(target, Quaternion.identity);
        }
    }
}
