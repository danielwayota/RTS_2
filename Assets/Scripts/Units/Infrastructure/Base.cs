using UnityEngine;
using System.Collections.Generic;

public class Base : Unit
{
    [Header("Unit positions")]
	public Transform spawnPoint;
	public Transform targetPoint;

	// JOBS STUFF
	protected Queue<Job> jobList;

	protected Job currentJob;

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
                    this.OnJobRetrievedFromQueue(currentJob);
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

            this.OnJobUpdate(this.currentJob);
		}

		this.OnUpdate();
	}
	// ========================================
	public void CreateUnit(string unitName)
	{
		UnitMetaData meta = UnitMetaStorage.current.GetUnitMetaByName(unitName);

		Job theJob = new Job(this.targetPoint.position, meta);

		this.jobList.Enqueue(theJob);

		this.OnJobCreated(theJob);
	}

    // ========================================
	protected virtual void OnUpdate() {}
    protected virtual void OnJobCreated(Job job) {}
    protected virtual void OnJobUpdate(Job job) {}
    protected virtual void OnJobRetrievedFromQueue(Job job) {}

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
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
	{
		this.targetPoint.position = worldPos;
	}
}
