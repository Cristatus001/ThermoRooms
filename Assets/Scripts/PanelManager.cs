using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;

    [Header("Congrats Panel")]
    public GameObject congratsPanel;
    public TextMeshProUGUI congratsFactText;
    public Button nextButton;
    public Button congratsHomeButton;

    [Header("Fail Panel")]
    public GameObject failPanel;
    public TextMeshProUGUI failFactText;
    public Button retryButton;
    public Button failHomeButton;

    [Header("Room Data")]
    public RoomData currentRoomData;

    [Header("Next Scene")]
    public string nextSceneName;
    public string mainMenuSceneName = "MainMenu";

    [Header("Transition")]
    public GameObject transitionPanel;
    public float transitionDuration = 1f;

    [Header("Game Complete Panel")]
public GameObject gameCompletePanel;
public TextMeshProUGUI gameCompleteFactText;
public Button playAgainButton;
public Button gameCompleteHomeButton;

[Header("Is Last Room")]
public bool isLastRoom = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
{
    nextButton.onClick.AddListener(OnNextPressed);
    congratsHomeButton.onClick.AddListener(OnHomePressed);
    retryButton.onClick.AddListener(OnRetryPressed);
    failHomeButton.onClick.AddListener(OnHomePressed);

    if (isLastRoom)
    {
        playAgainButton.onClick.AddListener(OnPlayAgainPressed);
        gameCompleteHomeButton.onClick.AddListener(OnHomePressed);
    }
}
    
    public void ShowCongratsPanel()
{
    string fact = GetRandomFact();

    if (isLastRoom)
    {
        AudioManager.Instance.PlayGameComplete();
        gameCompleteFactText.text = fact;
        gameCompletePanel.SetActive(true);
    }
    else
    {
        AudioManager.Instance.PlayRoomComplete();
        congratsFactText.text = fact;
        congratsPanel.SetActive(true);
    }
}

public void ShowFailPanel()
{
    AudioManager.Instance.PlayGameFail();
    string fact = GetRandomFact();
    failFactText.text = fact;
    failPanel.SetActive(true);
}

    string GetRandomFact()
    {
        if (currentRoomData == null || currentRoomData.factsData == null)
            return "";

        string[] facts = currentRoomData.factsData.facts;
        int randomIndex = Random.Range(0, facts.Length);
        return facts[randomIndex];
    }

    void OnNextPressed()
    {
        StartCoroutine(LoadSceneWithTransition(nextSceneName));
    }

    void OnRetryPressed()
    {
        LivesManager.Instance.ResetLives();
        StartCoroutine(LoadSceneWithTransition("Room1_Open"));
    }

    void OnHomePressed()
    {
        StartCoroutine(LoadSceneWithTransition(mainMenuSceneName));
    }
        void OnPlayAgainPressed()
    {
        LivesManager.Instance.ResetLives();
        StartCoroutine(LoadSceneWithTransition("Room1_Open"));
    }   

    IEnumerator LoadSceneWithTransition(string sceneName)
    {
        // Fade in transition panel
        transitionPanel.SetActive(true);
        CanvasGroup cg = transitionPanel.GetComponent<CanvasGroup>();
        
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, elapsed / transitionDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1f;

        SceneManager.LoadScene(sceneName);
    }
}