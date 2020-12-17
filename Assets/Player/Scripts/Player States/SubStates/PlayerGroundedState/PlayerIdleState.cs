public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(0.0f);
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
            else if (CrouchInput)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchIdleState);
            }
        }
    }
}
