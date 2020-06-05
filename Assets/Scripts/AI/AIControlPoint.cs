using UnityEngine;

using System.Collections.Generic;

public class AIControlPoint : MonoBehaviour
{
    public List<AIControlPoint> neighbours = new List<AIControlPoint>();

    // TODO: Sistema de alerta.

    private void OnDrawGizmos()
    {
        foreach (var node in this.neighbours)
        {
            Gizmos.DrawLine(this.transform.position, node.transform.position);
        }
    }
}
