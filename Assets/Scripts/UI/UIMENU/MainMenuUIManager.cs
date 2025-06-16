using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public GameObject newGameButton;
    public GameObject continueButton;
    public GameObject settingsButton;
    public GameObject exitButton;

    [Header("UI Text Elements")]
    public TextMeshProUGUI systemStatusText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI memoryText;
    public TextMeshProUGUI warningText;

    [Header("System Settings")]
    [Range(0f, 1f)] public float memoryCorruption = 0.45f; // 45% corrupted
    public bool systemUnstable = true;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateTime();
    }

    void UpdateUI()
    {
        // System Status
        systemStatusText.text = "SYSTEM STATUS: " + (systemUnstable ? "<mark=#8888>UNSTABLE</mark>" : "STABLE");

        // Memory
        int memPercent = Mathf.RoundToInt(memoryCorruption * 100f);
        memoryText.text = $"MEMORY: {memPercent}% CORRUPTED";

        // Warning
        warningText.text = systemUnstable ? "<color=red>WARNING: REALITY BREACH DETECTED</color>" : "";
    }

    void UpdateTime()
    {
        timeText.text = "TIME: " + System.DateTime.Now.ToString("HH:mm:ss");
    }

    // ===== BUTTON FUNCTIONS =====

    public void OnNewGame()
    {
        Debug.Log("Starting new game...");
        // Replace with your scene name
        SceneManager.LoadScene("GameScene"); 
    }

    public void OnContinue()
    {
        Debug.Log("Continue game...");
        // Load saved data here
    }

    public void OnSettings()
    {
        Debug.Log("Open Settings...");
        // Open settings panel
    }

    public void OnExit()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
