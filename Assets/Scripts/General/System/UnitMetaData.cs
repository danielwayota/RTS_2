using UnityEngine;

[System.Serializable]
public class UnitMetaData
{
    public string name;
    public GameObject prefab;
    public float requiredEnergy;

    public UnitMetaData() { }

    // ===========================================
    public UnitMetaData(string n, GameObject go, float re)
    {
        this.name = n;
        this.prefab = go;
        this.requiredEnergy = re;
    }

    /// ==========================================
    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="othermeta"></param>
    public UnitMetaData(UnitMetaData othermeta)
    {
        this.name = othermeta.name;
        this.prefab = othermeta.prefab;
        this.requiredEnergy = othermeta.requiredEnergy;
    }
}
