using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public TextMeshProUGUI storyText;       // Reference to a single UI text element
    public string[] storyChunks;            // Each chunk is a string
    private int index = 0;
    private float timer = 0f;
    public float delay = 6f;
    private bool isPaused = false;

    public SceneFader sceneFader;

    void Update()
    {
        if (!gameOverUI.activeSelf) return;

        timer += Time.unscaledDeltaTime;
        isPaused = true;
        SetCursorState(isPaused);

        if (timer >= delay)
        {
            timer = 0;
            index = (index + 1) % storyChunks.Length;
            if (storyText != null && storyChunks.Length > 0)
            {
                storyText.text = storyChunks[index];
            }
        }
    }

    private void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (storyText != null && storyChunks.Length > 0)
            storyText.text = storyChunks[0];

        if (EventSystem.current == null)
            Debug.LogWarning("No EventSystem found. Buttons won't work.");
    }

    public void ShowGameOverScreen()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        SetCursorState(true);
        StartCoroutine(DelayPause());
    }

    private IEnumerator DelayPause()
    {
        yield return new WaitForEndOfFrame(); // Let UI display first
        Time.timeScale = 0f;
    }

    public void OnRestart(string sceneName)
    {
        Debug.Log("OnRestart button pressed for scene: " + sceneName);
        Time.timeScale = 1f;

        if (sceneFader != null)
        {
            sceneFader.FadeToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnMainMenu(string sceneName)
    {
        Debug.Log("OnMainMenu button pressed for scene: " + sceneName);
        Time.timeScale = 1f;

        if (sceneFader != null)
        {
            sceneFader.FadeToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnExit()
    {
        Debug.Log("Exiting application.");
        Application.Quit();
    }

    void SetCursorState(bool showCursor)
    {
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }
}