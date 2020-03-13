using System.Collections.Generic;

using UnityEngine;

public class Sensor : MonoBehaviour
{
    [Header("Senses size")]
    public LayerMask unitsLayer;
    public float detectionDistance = 10;

    public bool useConeOfVision = false;
    public float maxViewDistance = 20;
    public float halfFieldOfView = 15;

    [Header("Config")]
    public Unit caller;
    public int colliderBufferSize;

    protected List<Unit> buffer;
    protected Collider[] colliderBuffer;

    /// =====================================
    private void Awake()
    {
        this.buffer = new List<Unit>();

        this.colliderBuffer = new Collider[this.colliderBufferSize];
    }

    /// =====================================
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public List<Unit> GetSensedUnits()
    {
        this.buffer.Clear();

        int count;

        count = Physics.OverlapSphereNonAlloc(
            this.transform.position,
            this.detectionDistance,
            this.colliderBuffer,
            this.unitsLayer
        );

        for (int i = 0; i < count; i++)
        {
            var collider = this.colliderBuffer[i];
            var unit = collider.GetComponent<Unit>();

            if (unit == null)
                continue;

            if (this.caller == unit)
                continue;

            this.buffer.Add(unit);
        }

        if (!this.useConeOfVision)
            return this.buffer;

        // Cone vision
        count = Physics.OverlapSphereNonAlloc(
            this.transform.position,
            this.maxViewDistance,
            this.colliderBuffer,
            this.unitsLayer
        );

        for (int i = 0; i < count; i++)
        {
            var collider = this.colliderBuffer[i];
            var unit = collider.GetComponent<Unit>();

            if (unit == null)
                continue;

            if (this.caller == unit)
                continue;

            if(this.buffer.Contains(unit))
                continue;

            Vector3 toEnemy = (unit.transform.position - this.transform.position).normalized;

            float angle = Mathf.Abs(Vector3.Angle(this.transform.forward, toEnemy));

            // ¿El enemigo está dentro del arco de visión?
            if (angle > this.halfFieldOfView)
                continue;

            this.buffer.Add(unit);
        }

        return this.buffer;
    }

    /// =====================================
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.detectionDistance);

        if (this.useConeOfVision)
        {
            Gizmos.DrawWireSphere(this.transform.position, this.maxViewDistance);

            float offsetX = Mathf.Sin(this.halfFieldOfView * Mathf.Deg2Rad);

            Vector3 left = new Vector3(offsetX, 0, 0);
            Vector3 right = new Vector3(-offsetX, 0, 0);

            left = (this.transform.forward + left).normalized;
            right = (this.transform.forward + right).normalized;

            Gizmos.DrawLine(
                this.transform.position,
                this.transform.position + (left * this.maxViewDistance)
            );

            Gizmos.DrawLine(
                this.transform.position,
                this.transform.position + (right * this.maxViewDistance)
            );
        }
    }
}
