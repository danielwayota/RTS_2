using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit")]
    public GameObject selectionMarker;

    [HideInInspector]
    public UnitManager manager;
    [HideInInspector]
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
    protected T RequireComponent<T>()
    {
        var component = this.GetComponent<T>();

        if (component == null)
        {
            component = this.GetComponentInChildren<T>();
        }

        if (component == null)
        {
            throw new System.Exception($"Component in {this.gameObject.name} not found");
        }

        return component;
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
