using UnityEngine;

[System.Serializable]
public class Job
{
    public UnitMetaData unitMeta;
    public Vector3 targetPosition;
    public Quaternion targetRotation;

    public float workDone;

    // ======================================
    public float progress
    {
        get { return this.workDone / this.unitMeta.requiredEnergy; }
    }

    // ======================================
    public bool isDone
    {
        get { return this.workDone >= this.unitMeta.requiredEnergy; }
    }

    // ======================================
    public string name
    {
        get { return this.unitMeta.name; }
    }

    // ======================================
    public Job() { }

    public Job(Vector3 pos, Quaternion rotation, UnitMetaData meta)
    {
        this.targetPosition = pos;
        this.targetRotation = rotation;
        this.unitMeta = new UnitMetaData(meta);
        this.workDone = 0;
    }
}