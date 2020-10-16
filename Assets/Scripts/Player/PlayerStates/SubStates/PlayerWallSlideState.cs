public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.SetVelocityY(-PlayerData.wallSlideVelocity);

            if (GrabInput && YInput == 0)
            {
                FiniteStateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}
