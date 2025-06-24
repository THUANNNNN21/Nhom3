using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject levelPanel;

    void Start()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void OpenLevelPanel()
    {
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void BackToMain()
    {
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Đã thoát game");
    }
}
