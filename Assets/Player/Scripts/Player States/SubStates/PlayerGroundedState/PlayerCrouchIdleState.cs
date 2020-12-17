public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
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
            if (!CrouchInput)
            {
                FiniteStateMachine.ChangeCurrentState(Player.IdleState);
            }
            else if (XInput != 0)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchMoveState);
            }
        }
    }
}
