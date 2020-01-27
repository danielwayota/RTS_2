using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject selectionMarker;

    public UnitManager manager;
    public Faction faction;

    /// <summary>
    /// Elementos visibles de la unidad que cambiarán al
    ///  color de la facción.
    /// </summary>
    public Renderer[] renderers;

    protected bool selected;
    public bool IsSelected
    {
        get { return this.selected; }
        set
        {
            // Mostrar u ocultar el icono de seleccion
            this.selectionMarker.SetActive(value);
            this.selected = value;
            this.OnSelectionChanged();
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

    public virtual void OnSelectionChanged() { }

    // ========================================
    // EXECUTE ORDER 66
    // ========================================
    public virtual void ExecuteOrder(Vector3 worldPos) { }
}
