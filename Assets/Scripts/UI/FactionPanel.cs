using UnityEngine;
using UnityEngine.UI;

public class FactionPanel : MonoBehaviour
{

    public Text energyStored;

	public void UpdateEnergy(int energy)
	{
		this.energyStored.text = energy.ToString();
	}
}
