public class AIFaction : Faction
{
    protected override void Awake()
    {
        var message = FindObjectOfType<FactionsInfo>();
        if (message != null)
        {
            this.materialColor = message.aiMaterial;
        }

        base.Awake();
    }
}
