using UnityEngine;
using UnityEngine.AI;

public class SolarGenerator : Unit
{
    private NavMeshAgent agent;

    private float time;
    private float timeOut = 1f;

    private Vector3 targetPosition;

    private Status status = Status.IDLE;

    private Animator animator;

    private EnergyGenerator generator;

    private void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.animator = GetComponent<Animator>();
        this.generator = GetComponent<EnergyGenerator>();   
    }

    // ================================
    public override void Init()
    {
        if (this.status != Status.IDLE)
        {
            this.agent.SetDestination(this.targetPosition);
        }
    }

    // ================================
    void Update()
    {
        this.time += Time.deltaTime;

        // Reloj
        if (this.time > this.timeOut)
        {
            this.time = 0;

            if (this.status == Status.MOVING)
            {
                if (!this.agent.hasPath || this.agent.velocity.sqrMagnitude == 0f)
                {
                    // Movimiento terminado
                    this.status = Status.IDLE;
                    this.animator.SetBool("IsOpen", true);
                    this.generator.TurnOn();
                }
            }

        }

    }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public override void ExecuteOrder(Vector3 worldPos)
    {
        this.targetPosition = worldPos;
        this.status = Status.MOVING;

        if (this.agent != null)
        {
            this.agent.SetDestination(worldPos);
        }
        this.animator.SetBool("IsOpen", false);
        this.generator.TurnOff();
    }
}
