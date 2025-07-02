using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneUIManager : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup zoneUIPanel;
    public TMP_Text zoneTitle;
    public TMP_Text timerText;
    public TMP_Text enemyProgressText;
    public TMP_Text enemyCountText;
    public TMP_Text progressMessage;
    public TMP_Text statusMessage;

    [Header("Settings")]
    public float fadeDuration = 0.5f;
    public float messageDisplayTime = 3f;

    private float fadeTimer;
    private float messageTimer;
    private bool isShowingMessage;
    private bool isPanelVisible;

    void Start()
    {
        // Start with UI hidden
        zoneUIPanel.alpha = 0;
        zoneUIPanel.interactable = false;
        zoneUIPanel.blocksRaycasts = false;
        isPanelVisible = false;
    }

    void Update()
    {
        // Handle message display timing
        if (isShowingMessage)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                statusMessage.text = "";
                isShowingMessage = false;
            }
        }
    }

    public void ShowZoneUI(Zone zone)
    {
        // Update UI content
        zoneTitle.text = zone.zoneName;
        enemyProgressText.text = "Enemies: ";
        enemyCountText.text = $"0/{zone.totalEnemies}";
        progressMessage.text = zone.startMessages;
        
        // Clear status message
        statusMessage.text = "";
        
        // Start fade in
        StartCoroutine(FadeUI(true));
    }

    public void HideZoneUI()
    {
        StartCoroutine(FadeUI(false));
    }

    private System.Collections.IEnumerator FadeUI(bool show)
    {
        if (show == isPanelVisible) yield break;
        
        isPanelVisible = show;
        float targetAlpha = show ? 1 : 0;
        float startAlpha = zoneUIPanel.alpha;
        float timer = 0;
        
        zoneUIPanel.interactable = show;
        zoneUIPanel.blocksRaycasts = show;
        
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            zoneUIPanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }
        
        zoneUIPanel.alpha = targetAlpha;
    }

    public void UpdateTimer(float timeRemaining)
    {
        // Format time as minutes:seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void UpdateEnemyCount(int defeated, int total)
    {
        enemyCountText.text = $"{defeated}/{total}";
        
        // Change color based on progress
        float progress = (float)defeated / total;
        enemyCountText.color = Color.Lerp(Color.red, Color.green, progress);
    }

    public void ShowProgressMessage(string message)
    {
        progressMessage.text = message;
    }

    public void ShowStatusMessage(string message, bool isSuccess)
    {
        statusMessage.text = message;
        statusMessage.color = isSuccess ? Color.green : Color.red;
        isShowingMessage = true;
        messageTimer = messageDisplayTime;
    }
}