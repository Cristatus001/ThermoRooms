using UnityEngine;
using UnityEngine.UI;

public class RoomUIRegistrar : MonoBehaviour
{
    public Image[] heartImages;

    void Start()
    {
        LivesManager.Instance.RegisterHeartsUI(heartImages);
    }
}