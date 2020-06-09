using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MainAIPlayer : Base
{
    private AIControlPoint[] controlPoints;
    private AIControlPoint baseControlPoint;

    private float timer = 0;
    private float timeOut = 0.5f;

    private int state;

    const int STATE_COUNT = 3;

    const int MAKE_UNITS = 0;
    const int EXPLORE = 1;
    const int ATTACK = 2;

    public Dictionary<AISquadType, AISquad> availableSquads;

    // Squad building
    private AISquad pendingSquad;

    private AISquadType[] squadBuildPriority = new AISquadType[] {
        AISquadType.ENERGY, AISquadType.EXPLORATION, AISquadType.ATTACK_LIGHT, AISquadType.ATTACK_HEAVY
    };

    // Exploration
    private AIControlPoint lastPointVisited;
    private int exploreCoolDown = 0;
    private int exploreMaxCoolDown = 16;

    // Attack
    private int attackCoolDown = 0;

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    public override void Init()
    {
        base.Init();

        this.controlPoints = FindObjectsOfType<AIControlPoint>();

        if (this.controlPoints.Length == 0)
        {
            Debug.LogError("There is no control points");
        }

        // Search nearest to base
        this.baseControlPoint = null;
        float recordDistance = float.MaxValue;

        foreach (var point in this.controlPoints)
        {
            var v = (point.transform.position - this.transform.position);

            float d = v.sqrMagnitude;
            if (d < recordDistance)
            {
                this.baseControlPoint = point;
                recordDistance = d;
            }
        }

        this.availableSquads = new Dictionary<AISquadType, AISquad>();

        this.lastPointVisited = this.baseControlPoint;
        this.ConnectControlPoints();
    }

    private void ConnectControlPoints()
    {
        foreach (var point in this.controlPoints)
        {
            // Buscar los dos más cercanos en referencia al actual.
            // Connectar

            var ordered = this.controlPoints
                .Where(
                    (p) => { return p != point; }
                )
                .OrderBy(
                    (AIControlPoint p) =>
                    {
                        return (point.transform.position - p.transform.position).sqrMagnitude;
                    }
                );

            foreach (var p in ordered)
            {
                point.neighbours.Add(p);
            }
        }
    }

    /// ===============================================
    /// <summary>
    /// Genera posiciones al rededor de la base.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetPositionInZone()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float range = Random.Range(8, 10);

        Vector3 offset = new Vector3(
            range * Mathf.Cos(angle),
            0,
            range * Mathf.Sin(angle)
        );

        return this.transform.position + offset;
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    protected override void OnUpdate()
    {
        this.timer += Time.deltaTime;

        if (this.timer >= this.timeOut)
        {
            this.timer -= this.timeOut;

            foreach (var alert in this.faction.alerts)
            {
                foreach (var control in this.controlPoints)
                {
                    if (control.isInRange(alert.origin))
                    {
                        control.alertLevel += 10;
                    }
                }
            }

            // Limpia los escuadrones vacíos.
            AISquad emptySquad = null;
            foreach (var item in this.availableSquads)
            {
                if (item.Value.isEmpty)
                {
                    emptySquad = item.Value;
                }
            }

            if (emptySquad != null)
            {
                this.availableSquads.Remove(emptySquad.type);
            }

            // Ejecutar el estado actual
            switch (this.state)
            {
                case MAKE_UNITS:
                    this.MakeUnits();
                    break;
                case EXPLORE:
                    this.Explore();
                    break;
                case ATTACK:
                    this.Attack();
                    break;
            }

            this.state = (this.state + 1) % STATE_COUNT;
        }
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    private void MakeUnits()
    {
        // Si hay trabajos en progreso, no hay necesidad de añadir más.
        if (this.jobList.Count > 0)
        {
            return;
        }

        if (this.pendingSquad != null)
        {
            if (this.pendingSquad.isComplete)
            {
                // El escuadrón está fabriado
                this.availableSquads[this.pendingSquad.type] = this.pendingSquad;
                this.pendingSquad = null;
            }

            return;
        }

        // Construir las unidades.
        foreach (var type in this.squadBuildPriority)
        {
            if (this.availableSquads.ContainsKey(type))
            {
                continue;
            }

            var data = AISquadConsts.Get(type);
            foreach (var unitName in data.unitNames)
            {
                this.targetPoint.position = this.GetPositionInZone();
                this.CreateUnit(unitName);
            }

            this.pendingSquad = new AISquad(type, data.unitNames.Length);

            return;
        }
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="unit"></param>
    protected override void OnUnitCreated(Unit unit)
    {
        if (this.pendingSquad == null)
        {
            Debug.LogError("The pending squad is null!");
            return;
        }

        this.pendingSquad.units.Add(unit);
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    private void Explore()
    {
        if (this.availableSquads.ContainsKey(AISquadType.EXPLORATION) == false)
        {
            return;
        }

        if (this.exploreCoolDown > 0)
        {
            this.exploreCoolDown--;
            return;
        }

        var squad = this.availableSquads[AISquadType.EXPLORATION];
        var point = this.lastPointVisited;

        var nextPointIndex = Random.Range(0, this.lastPointVisited.neighbours.Count - 1);
        var nextPoint = this.lastPointVisited.neighbours[nextPointIndex];

        squad.ExecuteOrder(nextPoint.transform.position);

        this.lastPointVisited = nextPoint;
        this.exploreCoolDown = this.exploreMaxCoolDown;
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    private void Attack()
    {
        if (this.attackCoolDown > 0)
        {
            this.attackCoolDown --;
            return;
        }

        IOrderedEnumerable<AIControlPoint> sortedByDanger = this.controlPoints.OrderByDescending((c) => c.alertLevel);

        foreach (var control in sortedByDanger)
        {
            if (control.alertLevel < 1)
                break;

            if (this.availableSquads.ContainsKey(AISquadType.ATTACK_HEAVY))
            {
                var squad = this.availableSquads[AISquadType.ATTACK_HEAVY];
                squad.ExecuteOrder(control.transform.position);

                this.availableSquads.Remove(AISquadType.ATTACK_HEAVY);

                this.attackCoolDown++;
            }
            else if (this.availableSquads.ContainsKey(AISquadType.ATTACK_LIGHT))
            {
                var squad = this.availableSquads[AISquadType.ATTACK_LIGHT];
                squad.ExecuteOrder(control.transform.position);

                this.availableSquads.Remove(AISquadType.ATTACK_LIGHT);

                this.attackCoolDown++;
            }
        }
    }
}
