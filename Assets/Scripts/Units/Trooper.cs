using UnityEngine;
using UnityEngine.AI;

public class Trooper : Unit
{
    private NavMeshAgent agent;
    // ================================
    public override void Init()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }

    // ================================
    void Update()
    {
        // Implementar combate
    }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.agent.SetDestination(worldPos);
    }
}
