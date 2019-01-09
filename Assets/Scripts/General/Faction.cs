using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

	public FactionPanel factionPanel;

	public float maxEnergy = 100000;

	public string factionName;
	public Material materialColor;

	[HideInInspector]
	public UnitManager unitManager;

	private float energy;

	void Awake()
	{
		this.unitManager = GetComponent<UnitManager>();		

		this.energy = 1000;

		if (this.factionPanel != null) 
		{
			this.factionPanel.UpdateEnergy(this.energy);
		}
	}

	public bool CanRetrieveEnergy(float amount)
	{
		return this.energy >= amount;
	}

	public float RetrieveEnergy(float amount)
	{
		if (amount < 0) { Debug.LogError("Trying to retrieve negative energy!"); return 0; }

		if (this.energy >= amount)
		{
			this.energy -= amount;
		}
		else
		{
			amount = this.energy;
			this.energy = 0;
		}

		if (this.factionPanel != null) 
		{
			this.factionPanel.UpdateEnergy(this.energy);
		}

		return amount;
	}

	public float StoreEnergy(float amount)
	{
		if (amount < 0) { Debug.LogError("Trying to store negative energy!"); return 0; }

		float spaceForEnergy = this.maxEnergy - this.energy;

		if (amount > spaceForEnergy)
		{
			this.energy += spaceForEnergy;

			amount = spaceForEnergy;
		}
		else
		{
			this.energy += amount;
		}

		if (this.factionPanel != null) 
		{
			this.factionPanel.UpdateEnergy(this.energy);
		}

		return amount;
	}
}
