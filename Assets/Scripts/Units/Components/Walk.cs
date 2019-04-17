using UnityEngine;
using UnityEngine.AI;

public enum WalkStatus
{
    IDLE,
    MOVING
}

[RequireComponent(typeof(NavMeshAgent))]
public class Walk : MonoBehaviour
{
    private NavMeshAgent agent;

    private float time;
    private float timeOut = 1f;

    private Vector3 targetPosition;

    public WalkStatus status { get; protected set; }

    // ===========================================
    void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }

    // ===========================================
    void Start()
    {
        if (this.status != WalkStatus.IDLE)
        {
            this.agent.SetDestination(this.targetPosition);
        }
    }

    // ===========================================
    void Update()
    {
        time += Time.deltaTime;

        if (time > timeOut)
        {
            time -= timeOut;

            if (this.status == WalkStatus.MOVING)
            {
                if (!this.agent.hasPath || this.agent.velocity.sqrMagnitude == 0f)
                {
                    // Movimiento terminado
                    this.status = WalkStatus.IDLE;
                }
            }
        }
    }

    // ===========================================
    public void SetDestination(Vector3 worldPos)
    {
        this.targetPosition = worldPos;
        this.status = WalkStatus.MOVING;

        if (this.agent != null)
        {
            this.agent.SetDestination(worldPos);
        }
    }
}
