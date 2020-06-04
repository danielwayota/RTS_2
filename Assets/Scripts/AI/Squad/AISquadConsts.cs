// ---------------------------------------------------------------
public enum AISquadType
{
    ENERGY, EXPLORATION, ATTACK_LIGHT, ATTACK_HEAVY
}

// ---------------------------------------------------------------
public class AISquadTemplate
{
    public AISquadType type;
    public string[] unitNames;

    public AISquadTemplate(AISquadType type, string[] unitNames)
    {
        this.type = type;
        this.unitNames = unitNames;
    }
}

// ---------------------------------------------------------------
public static class AISquadConsts
{
    public static AISquadTemplate[] templates;

    static AISquadConsts()
    {
        templates = new AISquadTemplate[]
        {
            new AISquadTemplate(
                AISquadType.ENERGY,
                new string[]{ "SolarGenerator", "SolarGenerator", "Mechanic" }
            ),
            new AISquadTemplate(
                AISquadType.EXPLORATION,
                new string[]{ "Trooper", "Trooper", "Trooper" }
            ),
            new AISquadTemplate(
                AISquadType.ATTACK_LIGHT,
                new string[]{
                    "Trooper", "Trooper", "Trooper", "Trooper", "Trooper",
                    "Mechanic", "Mechanic"
                }
            ),
            new AISquadTemplate(
                AISquadType.ATTACK_HEAVY,
                new string[]{
                    "Trooper", "Trooper", "Tank", "Trooper", "Trooper"
                }
            )
        };
    }

    public static AISquadTemplate Get(AISquadType type)
    {
        foreach (var squadTemplate in templates)
        {
            if (squadTemplate.type == type)
            {
                return squadTemplate;
            }
        }

        return null;
    }
}