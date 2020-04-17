public class Message
{
    /// Todos los nombres de mensages disponibles
    public const string UPDATE_FACTION_ENERGY = "UpdateFactionEnergy";

    /// ============================================
    public string name
    {
        protected set; get;
    }

    public object data
    {
        set; get;
    }

    /// ============================================
    public Message(string name, object data)
    {
        this.name = name;
        this.data = data;
    }
}
