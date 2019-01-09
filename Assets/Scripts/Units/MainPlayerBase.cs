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
				this.currentJob.unitMeta.requiredEnergy -= neededEnergy;
			}

			if (this.currentJob.unitMeta.requiredEnergy <= 0)
			{
				this.OnFinishedJob(this.currentJob);
				this.currentJob = null;
			}
		}
	}
	// ========================================
	public void CreateUnit(string unitName)
	{
		UnitMetaData meta = UnitMetaStorage.current.GetUnitMetaByName(unitName);

		Job theJob = new Job();
		theJob.unitMeta = new UnitMetaData();
		theJob.unitMeta.requiredEnergy = meta.requiredEnergy;
		theJob.unitMeta.prefab = meta.prefab;

		theJob.targetPosition = this.targetPoint.position;

		this.jobList.Enqueue(theJob);

		Debug.Log("Job Created: " + this.jobList.Count);
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

		u.faction = this.faction;
		
		u.ExecuteOrder(j.targetPosition);
		Debug.Log("Job terminated: " + this.jobList.Count);
	}

	// ========================================
	public override void OnSelectionChanged()
	{
		if (UIManager.current)
		{
			UIManager.current.ToggleMainBasePanel(this.selected);
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

[System.Serializable]
public class Job
{
	public UnitMetaData unitMeta;
	public Vector3 targetPosition;
}
