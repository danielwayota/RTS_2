using UnityEngine;

public class PlayerFaction : Faction
{
    protected FactionPanel factionPanel;

    protected override void Awake()
    {
        base.Awake();

        this.factionPanel = FindObjectOfType<FactionPanel>();

        this.factionPanel.UpdateEnergy(this.energy);
    }

    public override float RetrieveEnergy(float amount)
    {
        float e = base.RetrieveEnergy(amount);

        this.factionPanel.UpdateEnergy(this.energy);

        return e;
    }

    public override float StoreEnergy(float amount)
    {
        float stored = base.StoreEnergy(amount);

        this.factionPanel.UpdateEnergy(this.energy);

        return stored;
    }
}
