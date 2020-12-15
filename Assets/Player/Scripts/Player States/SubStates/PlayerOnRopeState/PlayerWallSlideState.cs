public class PlayerWallSlideState : PlayerTouchingWallState
{
    private D_PlayerWallSlideState _wallSlideData;

    public PlayerWallSlideState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerWallSlideState wallSlideData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _wallSlideData = wallSlideData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.SetVelocityY(-_wallSlideData.wallSlideVelocity);

            if (GrabInput && YInput == 0)
            {
                FiniteStateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}
