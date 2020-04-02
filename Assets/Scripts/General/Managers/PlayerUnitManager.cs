using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Player unit manager status
/// </summary>
public enum PUMStatus
{
    IDLE, SELECT, COMMAND
}

public class PlayerUnitManager : UnitManager
{
    public List<Unit> selectedUnits;
    public GameObject commandMarkerPrfb;


    [Header("Player Stuff")]
    // PROTOTYPE
    public Camera sceneCamera;
    public SelectionBox selectionBox;

    private Plane groundPlane;
    // END PROTOTYPE

    private PUMStatus status;

    private Vector3 commandPosition;
    private Quaternion commandRotation;

    private CommandMarker marker;

    /// ==============================================
    void Start()
    {
        this.groundPlane.SetNormalAndPosition(Vector3.up, Vector3.zero);

        this.status = PUMStatus.IDLE;
    }

    /// ==============================================
    void Update()
    {
        this.UnitSelectionRutine();

        this.UnitCommandRutine();
    }

    /// ==============================================
    public void UnitSelectionRutine()
    {
        if (this.status != PUMStatus.IDLE && this.status != PUMStatus.SELECT)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.status = PUMStatus.SELECT;
            this.selectionBox.Begin(Input.mousePosition);
        }

        if (this.status == PUMStatus.SELECT)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                // Arrastrando
                this.selectionBox.Drag(Input.mousePosition);

                // Seleccionar unidades

                if (this.selectionBox.IsValid())
                {
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            this.selectionBox.End();
            this.status = PUMStatus.IDLE;

            // La caja de selección es muy pequeña, se asume que ha sido un click.
            if (this.selectionBox.IsValid() == false)
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
                            if (this.units.Contains(u))
                            {
                                u.IsSelected = true;
                                this.selectedUnits.Add(u);
                            }
                        }
                    }
                }
            }

            UIManager.current.ClearSelection();
            UIManager.current.GetCurrentUnitSelection(this.selectedUnits);
        }
    }

    /// ==============================================
    /// <summary>
    ///
    /// </summary>
    public void UnitCommandRutine()
    {
        if (this.status != PUMStatus.IDLE && this.status != PUMStatus.COMMAND)
        {
            return;
        }

        if (this.selectedUnits.Count == 0)
            return;

        // Empezamos a comandar
        if (Input.GetMouseButtonDown(1))
        {
            this.status = PUMStatus.COMMAND;

            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            groundPlane.Raycast(ray, out distance);

            this.commandPosition = ray.GetPoint(distance);

            var go = Instantiate(this.commandMarkerPrfb, this.commandPosition, Quaternion.identity);

            this.marker = go.GetComponent<CommandMarker>();
        }

        if (this.status == PUMStatus.COMMAND)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
                float distance;
                groundPlane.Raycast(ray, out distance);

                Vector3 point = ray.GetPoint(distance);

                this.marker.RotateTo((point - this.commandPosition).normalized);
                this.commandRotation = this.marker.transform.rotation;
            }
        }

        // Terminamos de comandar
        if (Input.GetMouseButtonUp(1))
        {
            this.status = PUMStatus.IDLE;

            float index = 0;

            foreach (Unit u in this.selectedUnits)
            {
                Vector3 offset = Walk.GetPhyllotaxisOffsetByIndex(index, 2);

                u.ExecuteOrder(this.commandPosition + offset, this.commandRotation);

                index++;
            }

            this.marker.End();
        }
    }

    // ================================
    public override void RemoveUnit(Unit u)
    {
        this.units.Remove(u);
        this.selectedUnits.Remove(u);
    }
}
