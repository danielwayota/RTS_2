using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public GameObject selectionMarker;

    protected bool selected;

    // PROTOTYPE
    public UnitManager manager;

    public Faction faction;

    public Renderer[] renderers;

    public bool IsSelected
    {
        get { return this.selected; }
        set
        {
            // Mostrar u ocultar el icono de seleccion
            this.selectionMarker.SetActive(value);
            this.selected = value;
        }
    }
    // ========================================
    void Start()
    {
        this.IsSelected = false;

        this.manager = this.faction.unitManager;

        this.manager.units.Add(this);

        foreach(Renderer r in this.renderers)
        {
            r.material = this.faction.materialColor;
        }

        this.Init();
    }
    // ========================================
    void OnDestroy()
    {
        this.manager.RemoveUnit(this);
    }
    // ========================================
    public virtual void Init() { }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public virtual void ExecuteOrder(Vector3 worldPos) { }
}
