public class MainPlayerBase : Base, IMessageReceiver
{
    protected Message createJobMessage = new Message(Message.ENQUEUE_JOB, null);
    protected Message updateJobMessage = new Message(Message.UPDATE_JOB, null);
    protected Message removeJobMessage = new Message(Message.DEQUEUE_JOB, null);

    // =================================
    public override void Init()
    {
        base.Init();

        MessageBus.current.AddReceiver(Message.CREATE_UNIT, this);
    }

    // ========================================
    protected override void OnJobRetrievedFromQueue(Job job)
    {
        this.removeJobMessage.data = job;
        MessageBus.current.Send(this.removeJobMessage);
    }

    // ========================================
    protected override void OnJobCreated(Job job)
    {
        this.createJobMessage.data = job;
        MessageBus.current.Send(this.createJobMessage);
    }

    // ========================================
    protected override void OnJobUpdate(Job job)
    {
        if (this.IsSelected)
        {
            this.updateJobMessage.data = job;
            MessageBus.current.Send(this.updateJobMessage);
        }
    }

    /// ========================================
    public override void OnSelectionChanged()
    {
        if (UIManager.current)
        {
            if (this.IsSelected)
            {
                this.updateJobMessage.data = this.currentJob;
                MessageBus.current.Send(this.updateJobMessage);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="m"></param>
    public void Receive(Message m)
    {
        string unitName = (string)m.data;

        this.CreateUnit(unitName);
    }
}
