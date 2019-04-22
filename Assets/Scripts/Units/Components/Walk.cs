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

                Vector3 positionToCheck = this.targetPosition;

                int i = 0;
                while (this.IsTargetOccupied(positionToCheck) && i < 100)
                {
                    positionToCheck += this.GenerateNewPositionOffset();
                    i++;
                }

                if (i >= 90) { Debug.LogError("Too many iterations: " + i); }

                this.SetDestination(positionToCheck);
            }
        }
    }

    private Vector3 GenerateNewPositionOffset()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);

        float x = Mathf.Cos(angle) * this.agent.radius * 5f;
        float z = Mathf.Sin(angle) * this.agent.radius * 5f;

        return new Vector3(x, 0, z);
    }


    private bool IsTargetOccupied(Vector3 pos)
    {
        Collider[] objs = Physics.OverlapSphere(pos, this.agent.radius * 2, LayerMask.GetMask("Units"));
        bool isOccupied = true;

        if (objs.Length == 0)
        {
            isOccupied = false;
        }

        if (objs.Length == 1)
        {
            if (objs[0].gameObject == this.gameObject) { isOccupied = false; }
        }

        return isOccupied;
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
