using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public TextMeshProUGUI[] storyChunks;
    private int index = 0;
    private float timer = 0f;
    public float delay = 6f;
    public SceneFader sceneFader;
    void Update()
    {
        if (!gameOverUI.activeSelf) return;

        timer += Time.deltaTime;
        if (timer >= delay)
        {
            timer = 0;
            index = (index + 1) % storyChunks.Length;
            foreach (var chunk in storyChunks)
                chunk.gameObject.SetActive(false);
            storyChunks[index].gameObject.SetActive(true);
        }
    }


    private void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0f; // Pause game
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void OnRestart(string sceneName)
    {
        if (sceneFader != null)
        {
            sceneFader.FadeToScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Time.timeScale = 1f; // Ensure time resumes
            Debug.Log("Loading current scene: " + SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName); // Replace with your scene
        }
        // Time.timeScale = 1f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu( string sceneName)
    {
        if (sceneFader != null)
        {
            sceneFader.FadeToScene(sceneName);
        }
        else
        {
            Time.timeScale = 1f; // Ensure time resumes
            Debug.Log("Loading main menu scene: " + sceneName);
            SceneManager.LoadScene(sceneName); // Replace with your scene
        }
    }

    public void OnExit()
    {
        Application.Quit();
    }
}