using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerUnitManager : UnitManager {

	public List<Unit> selectedUnits;

	[Header("Player Stuff")]
	// PROTOTYPE
    public Camera sceneCamera;
    public SelectionBox selectionBox;

    private Plane groundPlane;
    // END PROTOTYPE


    private bool selecting;
	
	// ================================
    void Start()
    {
        this.groundPlane.SetNormalAndPosition(Vector3.up, Vector3.zero);

        this.selecting = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.selecting = true;
            this.selectionBox.Begin(Input.mousePosition);
        }

        if (this.selecting)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                // Arrastrando
                this.selectionBox.Drag(Input.mousePosition);

                // Seleccionar unidades

                foreach (Unit u in this.units)
                {
                    Vector2 screenCoord = this.sceneCamera.WorldToScreenPoint(u.transform.position);

                    if (this.selectionBox.selectionRect.Contains(screenCoord))
                    {
                        if (!u.IsSelected)
                        {
                            u.IsSelected = true;
                            this.selectedUnits.Add(u);
                        }
                    }
                    else
                    {
                        if (u.IsSelected)
                        {
                            u.IsSelected = false;
                            this.selectedUnits.Remove(u);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            this.selectionBox.End();
            this.selecting = false;

            if (!this.selectionBox.IsValid())
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    // Limpiar selección anterior
                    for (int i = 0; i < this.selectedUnits.Count; i++)
                    {
                        this.selectedUnits[i].IsSelected = false;
                    }
                    this.selectedUnits.Clear();

                    Ray ray = this.sceneCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        GameObject go = hit.collider.gameObject;

                        Unit u = go.GetComponent<Unit>();

                        if (u != null)
                        {
                            if (this.units.Contains(u)) {
                                u.IsSelected = true;
                                this.selectedUnits.Add(u);
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Calcular posicion de destino
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            groundPlane.Raycast(ray, out distance);
            Vector3 point = ray.GetPoint(distance);

            foreach (Unit u in this.selectedUnits)
            {
                u.ExecuteOrder(point);
            }
        }
    }
    // ================================
    public override void RemoveUnit(Unit u)
    {
        this.units.Remove(u);
        this.selectedUnits.Remove(u);
    }
}
