using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Walk))]
public class SolarGenerator : Unit
{
    private float time;
    private float timeOut = 1f;

    private Animator animator;

    private EnergyGenerator generator;
    private Walk walk;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.generator = GetComponent<EnergyGenerator>();   
        this.walk = GetComponent<Walk>();
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

            if (this.walk.status == WalkStatus.IDLE && !this.generator.isActive)
            {
                this.animator.SetBool("IsOpen", true);
                this.generator.TurnOn();
            }

        }

    }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.walk.SetDestination(worldPos);
        this.animator.SetBool("IsOpen", false);
        this.generator.TurnOff();
    }
}
