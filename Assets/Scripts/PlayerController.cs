using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InteractableObject nearbyObject;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void SetNearbyObject(InteractableObject obj)
    {
        nearbyObject = obj;
    }

    public void OnInteractButtonPressed()
    {
        if (nearbyObject != null)
        {
            PopupManager.Instance.ShowPopup(nearbyObject);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}