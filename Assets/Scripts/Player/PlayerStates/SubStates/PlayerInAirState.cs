using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;

    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isJumping;

    private bool _coyoteTime;

    public PlayerInAirState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        _xInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _grabInput = Player.InputHandler.GrabInput;

        CheckJumpHeightMultiplier();

        if (_isTouchingWall && _xInput == Player.FacingDirection && Player.CurrentVelocity.y <= 0)
        {
            FiniteStateMachine.ChangeState(Player.WallSlideState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            FiniteStateMachine.ChangeState(Player.JumpState);
        }
        else if (_isTouchingWall && _grabInput)
        {
            FiniteStateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_isGrounded & Player.CurrentVelocity.y < 0.01f)
        {
            FiniteStateMachine.ChangeState(Player.LandState);
        }
        else
        {
            Player.CheckIfShouldFlip(_xInput);
            Player.SetVelocityX(PlayerData.movementVelocity * _xInput);

            Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.MyAnmator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
        _isTouchingWall = Player.CheckIfTouchingWall();
    }

    private void CheckJumpHeightMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Player.CurrentVelocity.y <= 0)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time >= StartTime + PlayerData.coyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumps();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumping() => _isJumping = true;
}
