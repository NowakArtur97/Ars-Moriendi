public class PlayerMoveState : PlayerGroundedState
{
    private D_PlayerMoveState _moveStateData;

    public PlayerMoveState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerMoveState moveStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _moveStateData = moveStateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.CheckIfShouldFlip(XInput);
            Player.SetVelocityX(_moveStateData.movementVelocity * XInput);

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
}
