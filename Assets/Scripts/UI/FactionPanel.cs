using UnityEngine;
using UnityEngine.UI;

public class FactionPanel : MonoBehaviour
{
    public Text energyStored;

	// ======================================
	public void UpdateEnergy(float energy)
	{
		int intEnergy = (int)(energy);
		this.energyStored.text = intEnergy.ToString();
	}
}
