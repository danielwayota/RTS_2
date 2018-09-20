using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager current;

	public GameObject mainBasePanel;

	// ==============================
	void Start ()
	{
		current = this;
		this.mainBasePanel.SetActive(false);
	}

	// ==============================
	public void ToggleMainBasePanel(bool show)
	{
		this.mainBasePanel.SetActive(show);
	}
}
