using UnityEngine;

public class MapOrchestra : MonoBehaviour
{
    public GameObject ui;

    public GameObject unitMetaStorage;

    public GameObject aiFaction;
    public GameObject playerFaction;

    void Awake()
    {
        Instantiate(this.ui);

        Instantiate(this.unitMetaStorage);

        // Crear faciones
        Instantiate(this.aiFaction);
        Instantiate(this.playerFaction);
    }
}
