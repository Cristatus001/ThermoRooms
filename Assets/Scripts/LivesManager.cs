using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    [Header("Lives")]
    public int totalLives = 3;
    private int currentLives;

    [Header("UI - assigned per scene")]
    public Image[] heartImages;

    [Header("Player Blink")]
    public AudioSource flashSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentLives = totalLives;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHeartsUI(Image[] hearts)
    {
        heartImages = hearts;
        UpdateHeartsUI();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateHeartsUI();
        StartCoroutine(BlinkPlayer());

        if (currentLives <= 0)
        {
            PanelManager.Instance.ShowFailPanel();
        }
    }

    void UpdateHeartsUI()
{
    if (heartImages == null) return;

    for (int i = 0; i < heartImages.Length; i++)
    {
        if (heartImages[i] == null) continue; // skip destroyed references
        heartImages[i].enabled = i < currentLives;
    }
}

    public void ResetLives()
{
    currentLives = totalLives;
    heartImages = null; // clear stale references; new room will re-register via RoomUIRegistrar
}

    IEnumerator BlinkPlayer()
{
    AudioManager.Instance.PlayPlayerBlink();

    SpriteRenderer playerSprite = GameObject.FindWithTag("Player")?.GetComponent<SpriteRenderer>();

    if (playerSprite != null)
    {
        for (int i = 0; i < 4; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
}