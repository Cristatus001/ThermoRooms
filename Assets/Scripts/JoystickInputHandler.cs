using UnityEngine;

public class JoystickInputHandler : MonoBehaviour
{
    public FixedJoystick joystick;
    public PlayerController player;

    void Update()
    {
        Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical);
        player.SetMoveInput(input);
    }
}