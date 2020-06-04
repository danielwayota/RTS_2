using UnityEngine;
using System.Collections.Generic;

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

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    public override void Init()
    {
        base.Init();

        this.controlPoints = FindObjectsOfType<AIControlPoint>();

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

            // TODO: Limpiar los escuadrones vacíos

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

                // TODO: Añadir las unidades a los escuadrones.

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
                // TODO: Generar una posición de mejor manera.
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
        // TODO: Pillar el escuadrón de exploración y explorar.
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    private void Attack()
    {
        // TODO: Buscar los puntos con alerta y mandar unidades.
    }
}
