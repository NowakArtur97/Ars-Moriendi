public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.CheckIfShouldFlip(XInput);
            Player.SetVelocityX(PlayerData.movementVelocity * XInput);

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
