using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

	public string factionName;
	public Material materialColor;

	[HideInInspector]
	public UnitManager unitManager;

	void Awake()
	{
		this.unitManager = GetComponent<UnitManager>();		
	}
}
