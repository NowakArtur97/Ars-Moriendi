public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
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
            else if (CrouchInput)
            {
                FiniteStateMachine.ChangeState(Player.CrouchIdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
