using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    protected bool CrouchInput;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _dashInput;
    private bool _primaryAttackInput;
    private bool _secondaryAttackInput;

    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    public PlayerGroundedState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        CrouchInput = Player.InputHandler.CrouchInput;
        _jumpInput = Player.InputHandler.JumpInput;
        _grabInput = Player.InputHandler.GrabInput;
        _dashInput = Player.InputHandler.DashInput;
        _primaryAttackInput = Player.InputHandler.PrimaryAttackInput;
        _secondaryAttackInput = Player.InputHandler.SecondaryAttackInput;

        if (_primaryAttackInput)
        {
            FiniteStateMachine.ChangeState(Player.SwordAttackState01);
        }
        else if (_secondaryAttackInput && Player.FireArrowShotState.CheckIfCanShoot())
        {
            FiniteStateMachine.ChangeState(Player.FireArrowShotState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            FiniteStateMachine.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            FiniteStateMachine.ChangeState(Player.InAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            FiniteStateMachine.ChangeState(Player.WallGrabState);
        }
        else if (_dashInput && Player.DashState.CheckIfCanDash())
        {
            FiniteStateMachine.ChangeState(Player.DashState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
        _isTouchingWall = Player.CheckIfTouchingWall();
        _isTouchingLedge = Player.CheckIfTouchingLedge();
    }
}
