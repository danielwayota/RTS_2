using UnityEngine;

public class MapOrchestra : MonoBehaviour
{
    public GameObject ui;

    public GameObject unitMetaStorage;

    void Awake()
    {
        Instantiate(this.ui);

        Instantiate(this.unitMetaStorage);

        // TODO: Crear faciones
        // TODO: Crear bases y asignar faciones
    }
}
