using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager current;

	public MainBasePanel mainBasePanel;

	// ==============================
	void Start ()
	{
		current = this;
		this.mainBasePanel.gameObject.SetActive(false);
	}

	// ==============================
	public void ToggleMainBasePanel(bool show)
	{
		this.mainBasePanel.gameObject.SetActive(show);
	}
}
