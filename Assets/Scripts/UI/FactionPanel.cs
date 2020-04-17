using UnityEngine;
using UnityEngine.UI;

public class FactionPanel : MonoBehaviour, IMessageReceiver
{
    public Text energyStored;

    void Awake()
    {
        MessageBus.current.AddReceiver(Message.UPDATE_FACTION_ENERGY, this);
    }

    // ======================================
    public void UpdateEnergy(float energy)
    {
        int intEnergy = (int)(energy);
        this.energyStored.text = intEnergy.ToString();
    }

    public void Receive(Message m)
    {
        float energy = (float)m.data;
        this.UpdateEnergy(energy);
    }
}
