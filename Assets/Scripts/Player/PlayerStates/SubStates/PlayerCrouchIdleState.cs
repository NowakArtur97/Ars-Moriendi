public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
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
                FiniteStateMachine.ChangeState(Player.IdleState);
            }
            else if (XInput != 0)
            {
                FiniteStateMachine.ChangeState(Player.CrouchMoveState);
            }
        }
    }
}
