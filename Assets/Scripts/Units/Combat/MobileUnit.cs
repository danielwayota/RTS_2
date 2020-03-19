using UnityEngine;

public class MobileUnit : Unit
{
    [Header("Mobile Unit")]
    public bool shouldRotateOnStop = false;

    private Walk _walk;
    protected Walk walk
    {
        get
        {
            if (this._walk == null)
                this._walk = this.RequireComponent<Walk>();

            return this._walk;
        }
    }

    private float checkTimer = 0;
    private float checkTimeOut = .5f;

    private WalkStatus lastKnownWalkStatus;

    private Quaternion targetRotation = Quaternion.identity;

    /// ============================================
    /// <summary>
    ///
    /// </summary>
    public override void Init()
    {
        this.lastKnownWalkStatus = this.walk.status;
    }

    /// ============================================
    /// <summary>
    ///
    /// </summary>
    protected virtual void Update()
    {
        this.checkTimer += Time.deltaTime;
        if (this.checkTimer >= this.checkTimeOut)
        {
            this.checkTimer = 0;

            if (this.walk.status != this.lastKnownWalkStatus)
            {
                this.lastKnownWalkStatus = this.walk.status;

                this.StatusChanged();
            }
        }
    }

    protected void StatusChanged()
    {
        if (this.lastKnownWalkStatus == WalkStatus.IDLE && this.shouldRotateOnStop)
        {
            this.transform.rotation = this.targetRotation;
        }

        this.OnWalkStatusUpdated(this.lastKnownWalkStatus);
    }

    /// ============================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="status"></param>
    protected virtual void OnWalkStatusUpdated(WalkStatus status) {}

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos, Quaternion rotation)
    {
        this.walk.SetDestination(worldPos);
        this.targetRotation = rotation;
    }
}
