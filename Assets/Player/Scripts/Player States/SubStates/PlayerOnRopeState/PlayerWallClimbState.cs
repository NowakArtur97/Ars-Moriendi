public class PlayerWallClimbState : PlayerTouchingWallState
{
    private D_PlayerWallClimbState _wallClimbStateData;

    public PlayerWallClimbState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerWallClimbState wallClimbStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _wallClimbStateData = wallClimbStateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.SetVelocityY(_wallClimbStateData.wallClimbVelocity);

            if (YInput != 1)
            {
                FiniteStateMachine.ChangeCurrentState(Player.WallGrabState);
            }
        }
    }
}
