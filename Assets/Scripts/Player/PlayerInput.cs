using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public bool isJumpDown { get; private set; }
    public bool isHorizontalUp { get; private set; }
    public bool isHorizontalPressed { get; private set; }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isJumpDown = Input.GetButtonDown("Jump");
        isHorizontalUp = Input.GetButtonUp("Horizontal");
        isHorizontalPressed = Input.GetButton("Horizontal");
    }
}