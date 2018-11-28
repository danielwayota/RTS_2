using UnityEngine;

public class MainPlayerBase : Unit
{
	public GameObject trooperPrfb;

	[Header("Unit positions")]
	public Transform spawnPoint;
	public Transform targetPoint;

	// =================================
	public override void Init()
	{
		
	}

	public void CreateTrooper()
	{
		GameObject g = Instantiate(
			this.trooperPrfb,
			this.spawnPoint.position,
			Quaternion.identity
		);

		Unit u = g.GetComponent<Unit>();

		u.faction = this.faction;
		
		u.ExecuteOrder(this.targetPoint.position);
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
