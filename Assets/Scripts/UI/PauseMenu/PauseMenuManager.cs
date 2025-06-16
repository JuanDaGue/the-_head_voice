using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            Time.timeScale = pausePanel.activeSelf ? 0 : 1;
        }
    }

    public void OnResume() => TogglePause();
    public void OnExitGame() => Application.Quit();

    void TogglePause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}