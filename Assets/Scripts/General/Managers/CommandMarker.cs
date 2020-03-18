using System.Collections;

using UnityEngine;

public class CommandMarker : MonoBehaviour
{
    private bool shouldDeactivate = false;

    private float timeOut = 2f;

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    /// ===============================================
    IEnumerator MoveToDeactivate()
    {
        yield return new WaitForSeconds(this.timeOut);

        Vector3 init = this.transform.position;
        Vector3 target = this.transform.position + Vector3.down;

        float percent = 0;
        while (shouldDeactivate && percent < 1f)
        {
            percent += .1f;

            this.transform.position = Vector3.Lerp(init, target, percent);

            yield return null;
        }

        if (shouldDeactivate)
            this.gameObject.SetActive(false);
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="position"></param>
    public void Place(Vector3 position)
    {
        this.shouldDeactivate = false;
        this.gameObject.SetActive(true);
        this.transform.position = position;
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="direction"></param>
    public void RotateTo(Vector3 direction)
    {
        this.transform.LookAt(this.transform.position + direction);
    }

    /// ===============================================
    /// <summary>
    ///
    /// </summary>
    public void End()
    {
        this.shouldDeactivate = true;

        StopAllCoroutines();
        StartCoroutine(this.MoveToDeactivate());
    }
}
