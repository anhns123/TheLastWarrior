using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
    public void MoveScene()
    {
        SceneManager.LoadScene("MapSelect");
    }
}
