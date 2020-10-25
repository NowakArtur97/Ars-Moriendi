using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float _inputHoldTime = 0.2f;

    private PlayerInput _playerInput;
    private Camera mainCamera;

    private float _jumpInputStartTime;
    private float _dashInputStartTime;
    private float _primaryInputStartTime;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool CrouchInput { get; private set; }
    public bool PrimaryAttackInput { get; private set; }

    public int PrimaryAttackClickCount { get; private set; }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckPrimaryAttackInputHoldTime();
    }

    public void OnMoveInput(CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = Mathf.Abs(RawMovementInput.x) > 0.5f ? (int)(RawMovementInput * Vector2.right).normalized.x : 0;
        NormalizedInputY = Mathf.Abs(RawMovementInput.y) > 0.5f ? (int)(RawMovementInput * Vector2.up).normalized.y : 0;
    }

    public void OnJumpInput(CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _dashInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput(CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (_playerInput.currentControlScheme == "Keyboard and Mouse")
        {
            RawDashDirectionInput = mainCamera.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }
    }

    public void OnCrouchInput(CallbackContext context)
    {
        if (context.started)
        {
            CrouchInput = true;
        }
        if (context.canceled)
        {
            CrouchInput = false;
        }
    }

    public void OnPrimaryAttackInput(CallbackContext context)
    {
        if (context.started)
        {
            PrimaryAttackInput = true;
            _primaryInputStartTime = Time.time;
            PrimaryAttackClickCount = PrimaryAttackClickCount + 1 > 3 ? 1 : PrimaryAttackClickCount + 1;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;

    public void UsePrimaryAttackInput() => PrimaryAttackInput = false;

    public void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void CheckDashInputHoldTime()
    {
        if (Time.time >= _dashInputStartTime + _inputHoldTime)
        {
            DashInput = false;
        }
    }

    public void CheckPrimaryAttackInputHoldTime()
    {
        if (Time.time >= _primaryInputStartTime + _inputHoldTime)
        {
            PrimaryAttackClickCount = 0;
        }
    }
}
