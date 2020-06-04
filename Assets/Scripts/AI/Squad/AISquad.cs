using UnityEngine;
using System.Collections.Generic;

public class AISquad
{
    public AISquadType type;
    public List<Unit> units;

    protected int targetCount;

    public bool isEmpty
    {
        get
        {
            int length = this.units.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (this.units[i] == null)
                {
                    Debug.Log("NULL UNIT");
                    this.units.RemoveAt(i);
                }
            }

            return this.units.Count == 0;
        }
    }

    public bool isComplete
    {
        get => this.units.Count == this.targetCount;
    }

    public AISquad(AISquadType type, int targetCount)
    {
        this.type = type;
        this.units = new List<Unit>();

        this.targetCount = targetCount;
    }
}
