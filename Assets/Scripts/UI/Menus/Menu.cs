using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenu;
    public GameObject configMenu;

    [Header("Pickers")]
    public FacionColorPicker colorPicker;
    public MapPicker mapPicker;

    /// ==========================================
    /// <summary>
    ///
    /// </summary>
    void Start()
    {
        this.mainMenu.SetActive(true);
        this.configMenu.SetActive(false);
    }

    /// ==========================================
    public void GoToConfig()
    {
        this.mainMenu.SetActive(false);
        this.configMenu.SetActive(true);
    }

    /// ==========================================
    public void GoToMainMenu()
    {
        this.mainMenu.SetActive(true);
        this.configMenu.SetActive(false);
    }

    /// ==========================================
    public void StartGame()
    {
        Debug.Log("Start Game");
    }
}
