public class PlayerCrouchMoveState : PlayerGroundedState
{
    private D_PlayerCrouchMoveState _crouchMoveStateData;

    public PlayerCrouchMoveState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        D_PlayerCrouchMoveState crouchMoveStateData) : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _crouchMoveStateData = crouchMoveStateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.CheckIfShouldFlip(XInput);
        Player.SetVelocityX(_crouchMoveStateData.crouchMovementVelocity * XInput);

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
