public class MainPlayerBase : Base
{
    // ========================================
    protected override void OnJobRetrievedFromQueue(Job job)
    {
        UIManager.current.mainBasePanel.jobListUI.RemoveJob(job);
    }

    // ========================================
    protected override void OnJobCreated(Job job)
    {
        UIManager.current.mainBasePanel.jobListUI.AddJob(job);
    }

    // ========================================
    protected override void OnJobUpdate(Job job)
    {
        if (this.IsSelected)
        {
            UIManager.current.mainBasePanel.UpdateJobInfo(job);
        }
    }

    // ========================================
    public override void OnSelectionChanged()
    {
        if (UIManager.current)
        {
            UIManager.current.ToggleMainBasePanel(this.selected);

            if (this.IsSelected)
            {
                UIManager.current.mainBasePanel.UpdateJobInfo(this.currentJob);
            }
        }
    }
}
