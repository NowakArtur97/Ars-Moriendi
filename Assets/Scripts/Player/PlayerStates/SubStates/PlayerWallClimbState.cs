public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, D_PlayerData PlayerData, string _animationBoolName) : base(Player, FiniteStateMachine, PlayerData, _animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.SetVelocityY(PlayerData.wallClimbVelocity);

            if (YInput != 1)
            {
                FiniteStateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}
