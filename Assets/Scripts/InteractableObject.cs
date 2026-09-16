using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Object Data")]
    public string objectName;
    public bool isCorrectObject;
    public Sprite objectImage;

    [Header("References")]
    public GameObject interactLabel;

    private PlayerController player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            player.SetNearbyObject(this);
            interactLabel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetNearbyObject(null);
            player = null;
            interactLabel.SetActive(false);
        }
    }
}