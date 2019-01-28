using UnityEngine;
using System.Collections.Generic;

public class JobListUI : MonoBehaviour
{
    public GameObject jobUIPrefab;
    public float jobUIHeight = 30;

    private Dictionary<Job, JobUI> internalList;

    private void Awake()
    {
        this.internalList = new Dictionary<Job, JobUI>();
    }

    public void AddJob(Job j)
    {
        GameObject go = Instantiate(this.jobUIPrefab);
        go.transform.SetParent(this.transform);

        JobUI jobUI = go.GetComponent<JobUI>();

        jobUI.SetJob(j);
        this.internalList.Add(j, jobUI);

        RectTransform trans = this.GetComponent<RectTransform>();
        trans.sizeDelta = new Vector2(trans.sizeDelta.x, this.jobUIHeight * this.internalList.Count);
    }

    public void RemoveJob(Job j)
    {
        JobUI jobUI = this.internalList[j];
        this.internalList.Remove(j);

        Destroy(jobUI.gameObject);

        RectTransform trans = this.GetComponent<RectTransform>();
        trans.sizeDelta = new Vector2(trans.sizeDelta.x, this.jobUIHeight * this.internalList.Count);
    }
}
