public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.CheckIfShouldFlip(XInput);
        Player.SetVelocityX(PlayerData.movementVelocity * XInput);

        if (!IsExitingState)
        {
            if (XInput == 0)
            {
                FiniteStateMachine.ChangeState(Player.IdleState);
            }
            else if (CrouchInput)
            {
                FiniteStateMachine.ChangeState(Player.CrouchMoveState);
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
