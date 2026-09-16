using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [Header("Room Settings")]
    public int totalCorrectObjects = 5;
    private int collectedCount = 0;

    [Header("Top Panel Slots")]
    public Image[] objectSlots;
    public Sprite questionMarkSprite;

    [Header("Next Scene")]
    public string nextSceneName;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Initialize all slots with question mark
        for (int i = 0; i < objectSlots.Length; i++)
        {
            objectSlots[i].sprite = questionMarkSprite;
        }
    }

    public void ObjectCollected(InteractableObject collectedObject)
    {
        if (collectedCount < objectSlots.Length)
        {
            // Replace question mark with object image
            objectSlots[collectedCount].sprite = collectedObject.objectImage;
            collectedCount++;
        }

        if (collectedCount >= totalCorrectObjects)
        {
            RoomComplete();
        }
    }

       void RoomComplete()
    {
        AudioManager.Instance.PlayRoomComplete();
        PanelManager.Instance.ShowCongratsPanel();
    }
}