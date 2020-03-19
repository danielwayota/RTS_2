using UnityEngine;

[RequireComponent(typeof(Walk))]
public class SolarGenerator : MobileUnit
{
    [Header("Solar Generator")]
    private Animator animator;
    private EnergyGenerator generator;

    public override void Init()
    {
        base.Init();

        this.animator = GetComponent<Animator>();
        this.generator = GetComponent<EnergyGenerator>();
    }

    protected override void OnWalkStatusUpdated(WalkStatus status)
    {
        switch (status)
        {
            case WalkStatus.IDLE:
                this.animator.SetBool("IsOpen", true);
                this.generator.TurnOn();
                break;
            case WalkStatus.MOVING:
                this.animator.SetBool("IsOpen", false);
                this.generator.TurnOff();
                break;
        }
    }
}
