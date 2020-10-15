public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.CheckIfShouldFlip(XInput);
        Player.SetVelocityX(PlayerData.crouchMovementVelocity * XInput);

        if (!IsExitingState)
        {
            if (!CrouchInput)
            {
                FiniteStateMachine.ChangeState(Player.MoveState);
            }
            else if (XInput == 0)
            {
                FiniteStateMachine.ChangeState(Player.CrouchIdleState);
            }
        }
    }
}
