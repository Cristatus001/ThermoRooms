using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    [Header("SFX")]
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip correctCollectSFX;
    public AudioClip wrongPickSFX;
    public AudioClip playerBlinkSFX;
    public AudioClip buttonClickSFX;
    public AudioClip roomCompleteSFX;
    public AudioClip gameCompleteSFX;
    public AudioClip gameFailSFX;

    private bool musicOn = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Music controls
    public bool IsMusicOn() => musicOn;

    public void SetMusic(bool isOn)
    {
        musicOn = isOn;
        musicSource.mute = !isOn;
    }

    // SFX controls
    public void PlayCorrectCollect()
    {
        PlaySFX(correctCollectSFX);
    }

    public void PlayWrongPick()
    {
        PlaySFX(wrongPickSFX);
    }

    public void PlayPlayerBlink()
    {
        PlaySFX(playerBlinkSFX);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayRoomComplete()
    {
        PlaySFX(roomCompleteSFX);
    }

    public void PlayGameComplete()
    {
        PlaySFX(gameCompleteSFX);
    }

    public void PlayGameFail()
    {
        PlaySFX(gameFailSFX);
    }

    void PlaySFX(AudioClip clip)
    {
        if (clip == null) return; // silently skip if clip not assigned yet
        sfxSource.PlayOneShot(clip);
    }
}