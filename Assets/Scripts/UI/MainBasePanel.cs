using UnityEngine;

public class MainBasePanel : PanelBase
{
    public JobInfoUI currentJobUI;

    public JobListUI jobListUI;

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
