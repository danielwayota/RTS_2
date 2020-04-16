using System.Collections.Generic;

using UnityEngine;

enum UIType
{
    NONE, BASE, TROOPER, MIXED
}

public class UIManager : MonoBehaviour
{
    public static UIManager current;

    public MainBasePanel mainBasePanel;
    // public TrooperPanel trooperPanel;

    /// =======================================
    void Start()
    {
        current = this;
        this.ClearSelection();
    }

    /// =======================================
    public void ClearSelection()
    {
        this.mainBasePanel.SetActive(false);
        // this.trooperPanel.SetActive(false);
    }

    /// =======================================
    public void GetCurrentUnitSelection(List<Unit> currentSelected)
    {
        var targetType = UIType.NONE;

        foreach (var unit in currentSelected)
        {
            if (unit is Base)
            {
                if (targetType == UIType.NONE)
                {
                    targetType = UIType.BASE;
                }
                else if (targetType != UIType.BASE)
                {
                    targetType = UIType.MIXED;
                }
            }
            else if (unit is Trooper)
            {
                if (targetType == UIType.NONE)
                {
                    targetType = UIType.TROOPER;
                }
                else if (targetType != UIType.TROOPER)
                {
                    targetType = UIType.MIXED;
                }
            }
            else
            {
                if (targetType != UIType.NONE)
                {
                    targetType = UIType.MIXED;
                }
            }
        }

        switch (targetType)
        {
            case UIType.NONE:
            break;

            case UIType.BASE:
                this.mainBasePanel.SetActive(true);
            break;

            case UIType.TROOPER:
                // this.trooperPanel.SetActive(true);

                // Put avg health in UI
                // float sum = 0;
                // float count = 0;
                // foreach (var unit in currentSelected)
                // {
                //     var health = unit.GetComponent<Health>();

                //     sum += health.health / (float) health.maxHealth;

                //     count ++;
                // }

                // sum /= count;

                // this.trooperPanel.SetHealthPercent(sum);
            break;

            case UIType.MIXED:
                // TODO: Make mixed UI
            break;
        }
    }
}
