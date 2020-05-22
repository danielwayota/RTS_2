using UnityEngine;

public class Mechanic : MobileUnit
{
    [Header("Mechanic")]
    public GameObject healingEffect;

    public int healingPoints = 5;

    private float time;
    private float timeOut = 1f;

    private Sensor sensor;

    /// =============================================
    public override void Init()
    {
        base.Init();
        this.sensor = this.RequireComponent<Sensor>();
        this.sensor.caller = this;
    }

    /// =============================================
    protected override void Update()
    {
        base.Update();
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

            // Check near friend units

            var unitHealth = this.GetNearFriendToRepair();

            if (unitHealth != null)
            {
                this.transform.LookAt(unitHealth.transform.position);

                unitHealth.Restore(this.healingPoints);

                Instantiate(this.healingEffect, unitHealth.transform.position, Quaternion.identity);
            }
        }
    }

    /// =============================================
    /// <summary>
    ///
    /// </summary>
    public Health GetNearFriendToRepair()
    {
        float minHealthFound = float.MaxValue;
        Health nearFriendUnit = null;

        var posibleFriends = this.sensor.GetSensedUnits();

        foreach (var posibleFriend in posibleFriends)
        {
            if (posibleFriend.faction != this.faction)
                continue;

            var healthComp = posibleFriend.GetComponent<Health>();
            if (!healthComp.isDamaged)
                continue;

            if (healthComp.health < minHealthFound) {
                minHealthFound = healthComp.health;

                nearFriendUnit = healthComp;
            }
        }

        return nearFriendUnit;
    }
}
