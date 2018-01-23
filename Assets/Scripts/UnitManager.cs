using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    // PROTOTYPE
    public Camera sceneCamera;
    public SelectionBox selectionBox;

    private Plane groundPlane;
    // END PROTOTYPE

    public List<Unit> units;
    public List<Unit> selectedUnits;

    private bool selecting;

    // ================================
    void Start()
    {
        this.units = new List<Unit>();
        this.selectedUnits = new List<Unit>();

        this.groundPlane.SetNormalAndPosition(Vector3.up, Vector3.zero);

        this.selecting = false;
    }

    // ================================
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
}
