﻿using UnityEngine;
using UnityEngine.AI;

public enum WalkStatus
{
    IDLE,
    MOVING
}

[RequireComponent(typeof(NavMeshAgent))]
public class Walk : MonoBehaviour
{
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

    private Transform _destinationFlag;
    private Transform destinationFlag
    {
        get
        {
            if (this._destinationFlag == null)
            {
                var obj = new GameObject($"{this.gameObject.name} - Flag");

                var collider = obj.AddComponent(typeof(SphereCollider)) as SphereCollider;
                collider.radius = this.agent.radius;

                obj.layer = LayerMask.NameToLayer("UnitLocationFlag");

                this._destinationFlag = obj.transform;
            }

            return this._destinationFlag;
        }
    }

    /// ==============================================
    private bool IsTargetOccupied(Vector3 pos)
    {
        Collider[] objs = Physics.OverlapSphere(pos, this.agent.radius, LayerMask.GetMask("UnitLocationFlag"));
        bool isOccupied = true;

        if (objs.Length == 0)
        {
            isOccupied = false;
        }

        if (objs.Length == 1)
        {
            if (objs[0].gameObject == this.destinationFlag.gameObject)
            {
                isOccupied = false;
            }
        }

        return isOccupied;
    }

    /// ==============================================
    public void SetDestination(Vector3 worldPos)
    {
        var positionToCheck = worldPos;

        int i = 0;
        while (this.IsTargetOccupied(positionToCheck) && i < 100)
        {
            positionToCheck = worldPos + Walk.GetPhyllotaxisOffsetByIndex(i, this.agent.radius * 4);
            i++;
        }

        this.targetPosition = positionToCheck;
        this.SetDestinationFlag(positionToCheck);
        this.agent.SetDestination(this.targetPosition);
    }

    /// ==============================================
    private void SetDestinationFlag(Vector3 position)
    {
        this.destinationFlag.position = position;
    }

    /// ============================================
    /// <summary>
    /// Generates positions in a spiral.
    ///
    /// Source document: http://algorithmicbotany.org/papers/abop/abop-ch4.pdf
    /// </summary>
    /// <param name="index"></param>
    /// <param name="separation"></param>
    /// <returns></returns>
    public static Vector3 GetPhyllotaxisOffsetByIndex(float index, float separation = 1f)
    {
        float currentAngle = index * (137.5f * Mathf.Deg2Rad);
        float radius = separation * Mathf.Sqrt(index);

        float x = radius * Mathf.Cos(currentAngle);
        float z = radius * Mathf.Sin(currentAngle);

        return new Vector3(x, 0, z);
    }
}
