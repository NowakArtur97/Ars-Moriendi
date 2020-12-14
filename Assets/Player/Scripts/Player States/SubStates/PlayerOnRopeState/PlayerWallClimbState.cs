public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName)
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
