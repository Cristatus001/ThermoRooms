using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    [Header("Popup References")]
    public GameObject interactPopup;
    public TextMeshProUGUI objectNameText;
    public Image objectImageDisplay;
    public Button pickUpButton;
    public Button ignoreButton;

    private InteractableObject currentObject;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ignoreButton.onClick.AddListener(ClosePopup);
        pickUpButton.onClick.AddListener(OnPickUp);
    }

    public void ShowPopup(InteractableObject interactable)
    {
        currentObject = interactable;
        objectNameText.text = interactable.objectName;
        objectImageDisplay.sprite = interactable.objectImage;
        interactPopup.SetActive(true);
    }

    void OnPickUp()
{
    if (currentObject == null) return;

    if (currentObject.isCorrectObject)
    {
        AudioManager.Instance.PlayCorrectCollect();
        RoomManager.Instance.ObjectCollected(currentObject);
        currentObject.gameObject.SetActive(false);
    }
    else
    {
        AudioManager.Instance.PlayWrongPick();
        LivesManager.Instance.LoseLife();
    }

    ClosePopup();
}

    void ClosePopup()
    {
        interactPopup.SetActive(false);
        currentObject = null;
    }
}