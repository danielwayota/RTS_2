using UnityEngine;
using UnityEngine.UI;

public class JobInfoUI : MonoBehaviour
{
    public Text jobNameLabel;
    public Slider jobProgressSlider;

    /// ======================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

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
