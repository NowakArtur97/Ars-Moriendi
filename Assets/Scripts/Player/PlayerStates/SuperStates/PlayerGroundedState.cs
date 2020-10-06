using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;

    private bool _jumpInput;
    private bool _grabInput;

    private bool _isGrounded;
    private bool _isTouchingWall;

    public PlayerGroundedState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _grabInput = Player.InputHandler.GrabInput;

        if (_jumpInput && Player.JumpState.CanJump())
        {
            Player.InputHandler.UseJumpInput();
            FiniteStateMachine.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            FiniteStateMachine.ChangeState(Player.InAirState);
        }
        else if (_isTouchingWall && _grabInput)
        {
            FiniteStateMachine.ChangeState(Player.WallGrabState);
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
}
