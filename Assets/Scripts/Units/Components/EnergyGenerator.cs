using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
	public float productionRate = 10;

	public float maxBattery = 100;
	private float battery = 0;

	private Unit unit;

	private float time = 0;

	// ======================================
    void Start()
    {
		this.unit = GetComponent<Unit>();
		if (this.unit == null) { Debug.LogError("This isn't a valid Unit."); }
    }

	// ======================================
    void Update()
    {
		this.time += Time.deltaTime;

		this.battery += Time.deltaTime * this.productionRate;
		if (this.battery > this.maxBattery)
		{
			this.battery = this.maxBattery;
		}

		if (this.time >= 1.0f)
		{
			this.time = 0;
			this.battery -= this.unit.faction.StoreEnergy( Mathf.RoundToInt(this.battery) );
		}
    }
}
