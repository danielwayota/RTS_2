﻿using UnityEngine;

public class UnitMetaStorage : MonoBehaviour
{
    public static UnitMetaStorage current;

    public UnitMetaData[] unitMetaList;

    /// ====================================
    void Awake()
    {
        current = this;
    }

    /// ====================================
    public UnitMetaData GetUnitMetaByName(string name)
    {
        UnitMetaData unitMetaData = null;

        for (int i = 0; i < this.unitMetaList.Length; i++)
        {
            if (this.unitMetaList[i].name == name)
            {
                return this.unitMetaList[i];
            }
        }

        Debug.LogError($"<UnitMetaStorage> '{name}' not found!");
        return unitMetaData;
    }
}
