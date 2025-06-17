using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class LoadingScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitleText;
    public Slider loadingBar;
    public TextMeshProUGUI loadingPercentageText;
    public TextMeshProUGUI carouselText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI warningText;

    [Header("Carousel Texts")]
    [TextArea(3, 10)] public string[] historyTexts;
    public float carouselInterval = 5f;

    private float loadingProgress = 0f;
    private int currentCarouselIndex = 0;
    public SceneFader sceneFader;

    void Start()
    {

        titleText.text = "VOID";
        subtitleText.text = "Loading nightmare...";
        warningText.text = "";

        StartCoroutine(SimulateLoading());
        StartCoroutine(CarouselTextUpdate());
    }

    IEnumerator SimulateLoading()
    {
        while (loadingProgress < 1f)
        {
            loadingProgress += Time.deltaTime * 0.2f; // Simulate loading speed
            loadingBar.value = loadingProgress;
            loadingPercentageText.text = Mathf.RoundToInt(loadingProgress * 100f) + "%";
            UpdateStatsDisplay();
            yield return null;
        }

        loadingPercentageText.text = "100%";
        subtitleText.text = "Loading existential dread...";
        warningText.text = "<color=red>WARNING: PSYCHOLOGICAL DAMAGE IMMINENT</color>";
        if (sceneFader != null)
        {
            sceneFader.FadeToScene("PantanoLevel");
        }
        else
        {
            Debug.Log("Loading next scene: PantanoLevel");
            NextScene("PantanoLevel");
        }
    }

    IEnumerator CarouselTextUpdate()
    {
        while (true)
        {
            if (historyTexts.Length > 0)
            {
                carouselText.text = historyTexts[currentCarouselIndex];
                currentCarouselIndex = (currentCarouselIndex + 1) % historyTexts.Length;
            }
            yield return new WaitForSeconds(carouselInterval);
        }
    }

    void UpdateStatsDisplay()
    {
        int mem = Mathf.Clamp(Mathf.RoundToInt(80f * loadingProgress), 0, 80);
        int sanity = Mathf.Clamp(Mathf.RoundToInt(20f * loadingProgress), 0, 20);
        int hope = Mathf.Clamp(Mathf.RoundToInt(10f * loadingProgress), 0, 10);

        statsText.text = $"MEMORY: {mem}%   SANITY: {sanity}%   HOPE: {hope}%";
    }

    private void NextScene(string nameScene)
    {
        // Load the next scene here
        // Example: SceneManager.LoadScene("NextSceneName");
        Debug.Log("Loading next scene...");
        SceneManager.LoadScene(nameScene);
    }
}
