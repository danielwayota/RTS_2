using UnityEngine;

public class MainBasePanel : MonoBehaviour
{
    public JobInfoUI currentJobUI;

    public JobListUI jobListUI;

    // ======================================
    public void UpdateJobInfo(Job j)
    {
        if (j != null)
        {
            this.currentJobUI.gameObject.SetActive(true);
            this.currentJobUI.SetJobName(j.name);
            this.currentJobUI.SetJobProgress(j.progress);
        }
        else
        {
            this.currentJobUI.gameObject.SetActive(false);
        }
    }
}
