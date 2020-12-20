public class PlayerIdleState : PlayerGroundedState
{
    private D_PlayerIdleState _idleStateData;

    public PlayerIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerIdleState idleStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _idleStateData = idleStateData;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityZero();
        Player.SetBoxColliderHeight(_idleStateData.standColliderHeight);
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
            else if (CrouchInput || YInput == -1)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchIdleState);
            }
        }
    }
}
