using UnityEngine;
using UnityEngine.UI;

public class JobUI : MonoBehaviour
{
    public Text jobNameLabel;

    private Job jobData;

    public void SetJob(Job j)
    {
        this.jobData = j;
        this.jobNameLabel.text = j.unitMeta.name;
    }
}
