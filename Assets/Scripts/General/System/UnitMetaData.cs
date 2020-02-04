using UnityEngine;

[System.Serializable]
public class UnitMetaData
{
    public string name;
    public GameObject prefab;
    public float requiredEnergy;

    public UnitMetaData() { }

    // ======================================
    public UnitMetaData(string n, GameObject go, float re)
    {
        this.name = n;
        this.prefab = go;
        this.requiredEnergy = re;
    }
    /** ======================================
     * Copy constructor
     */
    public UnitMetaData(UnitMetaData othermeta)
    {
        this.name = othermeta.name;
        this.prefab = othermeta.prefab;
        this.requiredEnergy = othermeta.requiredEnergy;
    }

}