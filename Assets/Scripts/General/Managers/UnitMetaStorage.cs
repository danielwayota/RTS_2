using UnityEngine;

public class UnitMetaStorage : MonoBehaviour
{
    public static UnitMetaStorage current;

    public UnitMetaData[] unitMetaList;

    void Awake()
    {
        current = this;
    }

    public UnitMetaData GetUnitMetaByName(string name)
    {
        UnitMetaData unitMetaData = null;

        for (int i = 0; i < this.unitMetaList.Length; i++)
        {
            if (this.unitMetaList[i].name == name)
            {
                unitMetaData = this.unitMetaList[i];
            }
        }

        return unitMetaData;
    }
}

[System.Serializable]
public class UnitMetaData
{
    public string name;
    public GameObject prefab;
    public float requiredEnergy;

    public UnitMetaData(){}

    public UnitMetaData(string n, GameObject go, float re)
    {
        this.name = n;
        this.prefab = go;
        this.requiredEnergy = re;
    }
    /**
     * Copy constructor
     */
    public UnitMetaData(UnitMetaData othermeta)
    {
        this.name = othermeta.name;
        this.prefab = othermeta.prefab;
        this.requiredEnergy = othermeta.requiredEnergy;
    }

}