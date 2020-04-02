using UnityEngine;
using UnityEngine.UI;

public class JobInfoUI : PanelBase
{
    public Text jobNameLabel;
    public Slider jobProgressSlider;

    // ======================================
    public void SetJobName(string theName)
    {
        this.jobNameLabel.text = theName;
    }

    // ======================================
    public void SetJobProgress(float progress)
    {
        this.jobProgressSlider.value = Mathf.Clamp01(progress);
    }
}
