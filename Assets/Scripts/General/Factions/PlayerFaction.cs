using UnityEngine;

public class PlayerFaction : Faction
{
    protected Message updateEnergyMessage = new Message(Message.UPDATE_FACTION_ENERGY, 0f);

    protected override void Awake()
    {
        base.Awake();

        this.updateEnergyMessage.data = this.energy;
        MessageBus.current.Send(this.updateEnergyMessage);
    }

    public override float RetrieveEnergy(float amount)
    {
        float e = base.RetrieveEnergy(amount);

        this.updateEnergyMessage.data = this.energy;
        MessageBus.current.Send(this.updateEnergyMessage);

        return e;
    }

    public override float StoreEnergy(float amount)
    {
        float stored = base.StoreEnergy(amount);

        this.updateEnergyMessage.data = this.energy;
        MessageBus.current.Send(this.updateEnergyMessage);

        return stored;
    }
}
