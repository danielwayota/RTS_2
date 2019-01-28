using UnityEngine;

public class MainBasePanel : MonoBehaviour
{
    public JobInfoUI currentJobUI;

    public JobListUI jobListUI;

    public void UpdateJobInfo(Job j)
    {
        if (j != null)
        {
            this.currentJobUI.gameObject.SetActive(true);
            this.currentJobUI.SetJobName(j.unitMeta.name);
            this.currentJobUI.SetJobProgress(j.percentage);
        }
        else
        {
            this.currentJobUI.gameObject.SetActive(false);
        }
    }
}
