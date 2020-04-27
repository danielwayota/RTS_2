using UnityEngine;
using UnityEngine.SceneManagement;

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
        var nextMap = this.mapPicker.gameMapName;

        var dataTransfer = new GameObject();
        var factionInfo = dataTransfer.AddComponent(typeof(FactionsInfo)) as FactionsInfo;

        factionInfo.playerMaterial = this.colorPicker.playerMaterial;
        factionInfo.aiMaterial = this.colorPicker.aiMaterial;

        DontDestroyOnLoad(factionInfo);

        SceneManager.LoadScene(nextMap);
    }
}
