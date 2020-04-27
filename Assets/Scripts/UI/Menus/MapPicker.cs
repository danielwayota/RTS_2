using UnityEngine;
using UnityEngine.UI;

public class MapPicker : MonoBehaviour
{
    [Header("Current maps")]
    public string[] mapNames;

    [Header("UI")]
    public Text currentMap;
    public RectTransform sceneButtonHolders;

    [Header("Prefabs")]
    public GameObject sceneSelectBtn;

    public string gameMapName { get; protected set; }

    /// =========================================
    /// <summary>
    ///
    /// </summary>
    void Awake()
    {
        var randomMapBtn = Instantiate(this.sceneSelectBtn);
        randomMapBtn.transform.SetParent(this.sceneButtonHolders);

        randomMapBtn.GetComponentInChildren<Text>().text = "(?)";

        randomMapBtn.GetComponent<Button>().onClick.AddListener(() => {
            int rndIndex = Random.Range(0, this.mapNames.Length);

            int i = 0;
            while (this.gameMapName == this.mapNames[rndIndex] && i < 100)
            {
                rndIndex = Random.Range(0, this.mapNames.Length);
                i++;
            }

            this.SetMap(this.mapNames[rndIndex]);
        });

        int index = Random.Range(0, this.mapNames.Length);
        this.SetMap(this.mapNames[index]);

        // Generate map buttons
        foreach (var name in this.mapNames)
        {
            var mapBtn = Instantiate(this.sceneSelectBtn);
            mapBtn.transform.SetParent(this.sceneButtonHolders);
            mapBtn.GetComponentInChildren<Text>().text = name;

            var btn = mapBtn.GetComponent<Button>();

            var mapName = name + "";
            btn.onClick.AddListener(() => {
                this.SetMap(mapName);
            });
        }
    }

    /// =========================================
    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    private void SetMap(string name)
    {
        this.gameMapName = name;
        this.currentMap.text = name;
    }
}
