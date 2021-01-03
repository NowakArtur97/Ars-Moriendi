public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityZero();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (XInput != 0)
            {
                FiniteStateMachine.ChangeCurrentState(Player.MoveState);
            }
            else if (IsAnimationFinished)
            {
                FiniteStateMachine.ChangeCurrentState(Player.IdleState);
            }
        }
    }
}
