using UnityEngine;

public class MainAIPlayer : Unit
{
	public GameObject trooperPrfb;

	[Header("Unit positions")]
	public Transform spawnPoint;
	public Transform targetPoint;

	private float time;
	private float timeOut;

	private int waveUnits;
	private int currentUnits;

	// =================================
	public override void Init()
	{
		this.time = 0;
		this.timeOut = .5f;

		this.waveUnits = 128;
		this.currentUnits = 0;
	}
	
	// =================================
	void Update()
	{
		this.time += Time.deltaTime;

		if (this.time > this.timeOut)
		{
			this.time = 0;

			if (this.currentUnits < this.waveUnits)
			{
				if (this.faction.CanRetrieveEnergy(100))
				{
					GameObject newOne = Instantiate(
						this.trooperPrfb,
						this.spawnPoint.position,
						Quaternion.identity
					);

					Unit u = newOne.GetComponent<Unit>();

					u.faction = this.faction;

					u.ExecuteOrder(this.targetPoint.position);

					this.currentUnits++;

					this.faction.RetrieveEnergy(100);
				}
			}
		}
	}
}
