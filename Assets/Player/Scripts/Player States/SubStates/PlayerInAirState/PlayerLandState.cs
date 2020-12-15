public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (XInput != 0)
            {
                FiniteStateMachine.ChangeState(Player.MoveState);
            }
            else if (IsAnimationFinished)
            {
                FiniteStateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}
