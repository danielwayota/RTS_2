using UnityEngine;
using UnityEngine.UI;

public class TrooperPanel : PanelBase
{
    public Slider avgHealth;

    public void SetHealthPercent(float percent)
    {
        this.avgHealth.value = percent;
    }
}
