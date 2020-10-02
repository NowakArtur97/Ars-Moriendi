using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float _inputHoldTime = 0.2f;

    private float _jumpInputStartTime;

    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            _jumpInputStartTime = Time.time;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
