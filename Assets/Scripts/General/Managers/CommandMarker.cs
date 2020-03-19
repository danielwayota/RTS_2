using System.Collections;

using UnityEngine;

public class CommandMarker : MonoBehaviour
{
    /// ===============================================
    IEnumerator MoveToDeactivate()
    {
        yield return new WaitForSeconds(1f);

        Vector3 init = this.transform.position;
        Vector3 target = this.transform.position + Vector3.down * 2;

        float percent = 0;
        while (percent < 1f)
        {
            percent += .01f;

            this.transform.position = Vector3.Lerp(init, target, percent);

            yield return null;
        }

        Destroy(this.gameObject);
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
        StartCoroutine(this.MoveToDeactivate());
    }
}
