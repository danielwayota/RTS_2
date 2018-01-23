using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    public Rect selectionRect;

    private RectTransform rectTransform;

    private Vector3 mouseStart;


    // ================================
    void Start()
    {
        this.selectionRect.Set(0, 0, 0, 0);

        this.rectTransform = GetComponent<RectTransform>();
        this.rectTransform.gameObject.SetActive(false);
    }

    // ================================
    public void Begin(Vector3 mousePos)
    {
        this.mouseStart = mousePos;

        this.selectionRect.Set(mousePos.x, mousePos.y, 0, 0);

        this.rectTransform.gameObject.SetActive(true);

        this.rectTransform.offsetMin = this.selectionRect.min;
        this.rectTransform.offsetMax = this.selectionRect.max;
    }

    // ================================
    public void Drag(Vector3 mousePos)
    {
        // Arrastrando hacia la izquierda.
        if (mousePos.x < this.mouseStart.x)
        {
            this.selectionRect.xMin = mousePos.x;
            this.selectionRect.xMax = this.mouseStart.x;
        }
        // Arrastrando hacia la derecha.
        else
        {
            this.selectionRect.xMin = this.mouseStart.x;
            this.selectionRect.xMax = mousePos.x;
        }

        // Arrastrando hacia abajo.
        if (mousePos.y < this.mouseStart.y)
        {
            this.selectionRect.yMin = mousePos.y;
            this.selectionRect.yMax = this.mouseStart.y;
        }
        // Arrastrando hacia arriba
        else
        {
            this.selectionRect.yMin = this.mouseStart.y;
            this.selectionRect.yMax = mousePos.y;
        }

        this.rectTransform.offsetMin = this.selectionRect.min;
        this.rectTransform.offsetMax = this.selectionRect.max;
    }

    // ================================
    public void End()
    {
        this.rectTransform.gameObject.SetActive(false);
    }
}
