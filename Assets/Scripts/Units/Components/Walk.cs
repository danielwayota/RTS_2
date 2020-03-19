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
    private float time;
    private float timeOut = 1f;

    private Vector3 targetPosition;

    private NavMeshAgent _agent;
    private NavMeshAgent agent
    {
        get
        {
            if (this._agent == null)
            {
                this._agent = GetComponent<NavMeshAgent>();
            }
            return this._agent;
        }

    }

    public WalkStatus status
    {
        get
        {
            if (this.agent.velocity.sqrMagnitude <= 0.1f)
                return WalkStatus.IDLE;

            return WalkStatus.MOVING;
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
        this.agent.SetDestination(this.targetPosition);
    }
}
