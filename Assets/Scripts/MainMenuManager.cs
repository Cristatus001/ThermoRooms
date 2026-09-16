using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;
    public Button musicButton;

    [Header("Settings Panel")]
    public GameObject settingsPanel;

    [Header("Music Button Visuals")]
    public Image musicIcon;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public TextMeshProUGUI musicButtonText; // optional, leave empty if not used

    [Header("Scene")]
    public string firstRoomScene = "Room1_Open";

    private bool settingsOpen = false;

    void Start()
{
    playButton.onClick.AddListener(PlayButtonClick);
    playButton.onClick.AddListener(OnPlayPressed);

    settingsButton.onClick.AddListener(PlayButtonClick);
    settingsButton.onClick.AddListener(ToggleSettingsPanel);

    exitButton.onClick.AddListener(PlayButtonClick);
    exitButton.onClick.AddListener(OnExitPressed);

    musicButton.onClick.AddListener(PlayButtonClick);
    musicButton.onClick.AddListener(OnMusicButtonPressed);

    settingsPanel.SetActive(false);
    UpdateMusicButtonVisual();
}
    void PlayButtonClick()
    {   
        AudioManager.Instance.PlayButtonClick();
    }
    void OnPlayPressed()
{
    LivesManager.Instance.ResetLives();
    SceneManager.LoadScene(firstRoomScene);
    AudioManager.Instance.PlayButtonClick();
}

    void ToggleSettingsPanel()
    {
        settingsOpen = !settingsOpen;
        settingsPanel.SetActive(settingsOpen);
        AudioManager.Instance.PlayButtonClick();
    }

    void OnMusicButtonPressed()
    {
        bool newState = !AudioManager.Instance.IsMusicOn();
        AudioManager.Instance.SetMusic(newState);
        UpdateMusicButtonVisual();
        AudioManager.Instance.PlayButtonClick();
    }

    void UpdateMusicButtonVisual()
    {
        bool isOn = AudioManager.Instance.IsMusicOn();

        if (musicIcon != null)
            musicIcon.sprite = isOn ? musicOnSprite : musicOffSprite;

        if (musicButtonText != null)
            musicButtonText.text = isOn ? "MUSIC: ON" : "MUSIC: OFF";
    }

    void OnExitPressed()
    {
        Application.Quit();
        AudioManager.Instance.PlayButtonClick();
    }
}