using System.Collections.Generic;
using UnityEngine;

public class MainPlayerBase : Unit
{
	public GameObject trooperPrfb;

	[Header("Unit positions")]
	public Transform spawnPoint;
	public Transform targetPoint;

	// JOBS STUFF
	private Queue<Job> jobList;

	private Job currentJob;

	private float jobCheckTime = 0.5f;
	private float jobTimer = 0;

	private float workingSpeed = 50;

	// =================================
	public override void Init()
	{
		this.jobList = new Queue<Job>();
	}
	// ========================================
	private void Update()
	{
		this.jobTimer += Time.deltaTime;

		// Check for new Jobs
		if (this.jobTimer > this.jobCheckTime)
		{
			if (currentJob == null)
			{
				if (this.jobList.Count > 0)
				{
					currentJob = this.jobList.Dequeue();
					UIManager.current.mainBasePanel.jobListUI.RemoveJob(currentJob);
				}
			}

			this.jobTimer = 0;
		}

		// Work on Job
		if (this.currentJob != null)
		{
			float neededEnergy = this.workingSpeed * Time.deltaTime;

			if (this.faction.CanRetrieveEnergy(neededEnergy))
			{
				this.faction.RetrieveEnergy(neededEnergy);
				this.currentJob.workDone += neededEnergy;
			}

			if (this.currentJob.isDone)
			{
				this.OnFinishedJob(this.currentJob);
				this.currentJob = null;
			}

			if (this.IsSelected)
			{
				UIManager.current.mainBasePanel.UpdateJobInfo(this.currentJob);
			}
		}
	}
	// ========================================
	public void CreateUnit(string unitName)
	{
		UnitMetaData meta = UnitMetaStorage.current.GetUnitMetaByName(unitName);

		Job theJob = new Job(this.targetPoint.position, meta);

		this.jobList.Enqueue(theJob);
		UIManager.current.mainBasePanel.jobListUI.AddJob(theJob);
	}
	// ========================================
	private void OnFinishedJob(Job j)
	{
		GameObject g = Instantiate(
			j.unitMeta.prefab,
			this.spawnPoint.position,
			Quaternion.identity
		);

		Unit u = g.GetComponent<Unit>();

		if (u == null) {
			Debug.LogError("This object has no Unit component");
			return;
		}

		u.faction = this.faction;
		
		u.ExecuteOrder(j.targetPosition);
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

	// ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
	{
		this.targetPoint.position = worldPos;
	}
}
