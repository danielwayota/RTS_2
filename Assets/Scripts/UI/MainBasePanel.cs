using UnityEngine;

public class MainBasePanel : MonoBehaviour
{
    public JobInfoUI currentJobUI;

    public JobListUI jobListUI;

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
    public void UpdateJobInfo(Job j)
    {
        if (j != null)
        {
            this.currentJobUI.SetActive(true);
            this.currentJobUI.SetJobName(j.name);
            this.currentJobUI.SetJobProgress(j.progress);
        }
        else
        {
            this.currentJobUI.SetActive(false);
        }
    }
}
