using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour
{
    public Button crystalCaveButton;
    public Button snowfieldButton;
    public Button volcanoesButton;
    public Button confirmButton;

    private string selectedMap = "";

    void Start()
    {
        confirmButton.interactable = false;

        crystalCaveButton.onClick.AddListener(() => SelectMap("CrystalCave"));
        snowfieldButton.onClick.AddListener(() => SelectMap("Snowfield"));
        volcanoesButton.onClick.AddListener(() => SelectMap("Volcanoes"));
        confirmButton.onClick.AddListener(LoadSelectedMap);
    }

    void SelectMap(string mapName)
    {
        selectedMap = mapName;
        confirmButton.interactable = true;
        Debug.Log("Selected map: " + mapName);
    }

    void LoadSelectedMap()
    {
        if (selectedMap == "CrystalCave")
            SceneManager.LoadScene("CrystalCave");
        else if (selectedMap == "Snowfield")
            SceneManager.LoadScene("Snowfield");
        else if (selectedMap == "Volcanoes")
            SceneManager.LoadScene("Volcanoes");
        else
            Debug.LogWarning("No map selected!");
    }
}
