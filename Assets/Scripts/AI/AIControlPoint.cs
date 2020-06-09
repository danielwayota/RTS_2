using UnityEngine;

using System.Collections.Generic;

public class AIControlPoint : MonoBehaviour
{
    public List<AIControlPoint> neighbours = new List<AIControlPoint>();

    public float alertRange = 2;

    public float alertLevel;

    /// ==========================================
    /// <summary>
    ///
    /// </summary>
    void Update()
    {
        this.alertLevel -= Time.deltaTime;

        if (this.alertLevel < 0)
        {
            this.alertLevel = 0;
        }
    }

    /// ==========================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool isInRange(Vector3 position)
    {
        var distance = (this.transform.position - position).sqrMagnitude;
        var sqrRange = this.alertRange * this.alertRange;

        return distance <= sqrRange;
    }

    /// ==========================================
    /// <summary>
    ///
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.alertRange);

        foreach (var node in this.neighbours)
        {
            Gizmos.DrawLine(this.transform.position, node.transform.position);
        }
    }
}
