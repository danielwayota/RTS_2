using UnityEngine;
using UnityEngine.UI;

public class FacionColorPicker : MonoBehaviour
{
    [Header("Material list")]
    public Material[] materialList;

    [Header("Current colors")]
    public Image playerCurrentColor;
    public Image aiCurrentColor;

    [Header("Containers")]
    public RectTransform playerColorBtnContainer;
    public RectTransform aiColorBtnContainer;

    [Header("Prefabs")]
    public GameObject buttonPrefab;

    public Material playerMaterial { get; protected set; }
    public Material aiMaterial { get; protected set; }

    /// ================================================
    /// <summary>
    ///
    /// </summary>
    void Awake()
    {
        int index = 0;
        foreach (var material in this.materialList)
        {
            var color = material.color;

            var playerGOBtn = Instantiate(this.buttonPrefab);
            var aiGOBtn = Instantiate(this.buttonPrefab);

            playerGOBtn.GetComponent<Image>().color = color;
            aiGOBtn.GetComponent<Image>().color = color;

            this.SetPlayerColorButtonEvent(playerGOBtn.GetComponent<Button>(), index);
            this.SetAIColorButtonEvent(aiGOBtn.GetComponent<Button>(), index);

            playerGOBtn.transform.SetParent(this.playerColorBtnContainer);
            aiGOBtn.transform.SetParent(this.aiColorBtnContainer);

            index++;
        }

        int playerRandomColor = Random.Range(0, this.materialList.Length);
        int aiRandomColor = Random.Range(0, this.materialList.Length);

        this.playerMaterial = this.materialList[playerRandomColor];
        this.aiMaterial = this.materialList[aiRandomColor];

        this.playerCurrentColor.color = this.playerMaterial.color;
        this.aiCurrentColor.color = this.aiMaterial.color;
    }

    /// ================================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="materialIndex"></param>
    private void SetPlayerColorButtonEvent(Button btn, int materialIndex)
    {
        int index = materialIndex;
        btn.onClick.AddListener(() => {
            this.playerMaterial = this.materialList[index];

            this.playerCurrentColor.color = this.playerMaterial.color;
        });
    }

    /// ================================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="materialIndex"></param>
    private void SetAIColorButtonEvent(Button btn, int materialIndex)
    {
        int index = materialIndex;
        btn.onClick.AddListener(() => {
            this.aiMaterial = this.materialList[index];

            this.aiCurrentColor.color = this.aiMaterial.color;
        });
    }
}
