public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, D_PlayerData PlayerData, string _animationBoolName) : base(Player, FiniteStateMachine, PlayerData, _animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityX(0.0f);
        Player.SetVelocityY(0.0f);

        if (YInput > 0)
        {
            FiniteStateMachine.ChangeState(Player.WallClimbState);
        }
        else if (YInput < 0 && !GrabInput)
        {
            FiniteStateMachine.ChangeState(Player.WallSlideState);
        }
    }
}
