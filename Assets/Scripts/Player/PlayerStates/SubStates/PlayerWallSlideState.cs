public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, D_PlayerData PlayerData, string _animationBoolName) : base(Player, FiniteStateMachine, PlayerData, _animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityY(-PlayerData.wallSlideVelocity);

        if (GrabInput && YInput == 0)
        {
            FiniteStateMachine.ChangeState(Player.WallGrabState);
        }
    }
}
