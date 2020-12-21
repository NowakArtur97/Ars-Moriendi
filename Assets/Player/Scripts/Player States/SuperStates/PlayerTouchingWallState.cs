public abstract class PlayerTouchingWallState : PlayerState
{
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingLedge;
    protected int XInput;
    protected int YInput;
    protected bool GrabInput;
    protected bool JumpInput;

    public PlayerTouchingWallState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, string _animationBoolName)
        : base(Player, FiniteStateMachine, _animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        YInput = Player.InputHandler.NormalizedInputY;
        GrabInput = Player.InputHandler.GrabInput;
        JumpInput = Player.InputHandler.JumpInput;

        if (JumpInput)
        {
            Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            FiniteStateMachine.ChangeCurrentState(Player.WallJumpState);
        }
        else if (IsGrounded && !GrabInput)
        {
            FiniteStateMachine.ChangeCurrentState(Player.IdleState);
        }
        else if (!IsTouchingWall || (XInput != Player.FacingDirection && !GrabInput))
        {
            FiniteStateMachine.ChangeCurrentState(Player.InAirState);
        }
        else if (IsTouchingWall && !IsTouchingLedge)
        {
            FiniteStateMachine.ChangeCurrentState(Player.LedgeClimbState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
        IsTouchingWall = Player.CheckIfTouchingWall();
        IsTouchingLedge = Player.CheckIfTouchingLedge();

        if (IsTouchingWall && !IsTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
        }
    }
}
