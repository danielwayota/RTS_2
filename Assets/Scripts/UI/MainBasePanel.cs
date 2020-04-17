using UnityEngine;

public class MainBasePanel : PanelBase, IMessageReceiver
{
    public JobInfoUI currentJobUI;

    public JobListUI jobListUI;

    protected Message createUnitJob = new Message(Message.CREATE_UNIT, "");

    /// ========================================
    void Awake()
    {
        MessageBus.current.AddReceiver(Message.UPDATE_JOB, this);
        MessageBus.current.AddReceiver(Message.ENQUEUE_JOB, this);
        MessageBus.current.AddReceiver(Message.DEQUEUE_JOB, this);

        this.gameObject.SetActive(false);
    }

    /// ========================================
    public void SendCreateUnitMessage(string unitName)
    {
        this.createUnitJob.data = unitName;
        MessageBus.current.Send(this.createUnitJob);
    }

    /// ========================================
    public void Receive(Message m)
    {
        Job j = (Job)m.data;

        switch (m.name)
        {
            case Message.ENQUEUE_JOB:
                this.jobListUI.AddJob(j);
                break;
            case Message.UPDATE_JOB:
                this.UpdateJob(j);
                break;
            case Message.DEQUEUE_JOB:
                this.jobListUI.RemoveJob(j);
                break;
        }
    }

    /// ========================================
    protected void UpdateJob(Job j)
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
