using UnityEngine;

public class PanelBase : MonoBehaviour
{
    /// ======================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}