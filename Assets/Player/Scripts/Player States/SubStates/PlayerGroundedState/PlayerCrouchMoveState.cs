﻿public class PlayerCrouchMoveState : PlayerGroundedState
{
    private D_PlayerCrouchMoveState _crouchMoveStateData;

    public PlayerCrouchMoveState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        D_PlayerCrouchMoveState crouchMoveStateData) : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _crouchMoveStateData = crouchMoveStateData;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetBoxColliderHeight(_crouchMoveStateData.crouchColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.CheckIfShouldFlip(XInput);
        Player.SetVelocityX(_crouchMoveStateData.crouchMovementVelocity * XInput);

        if (!IsExitingState)
        {
            if (!CrouchInput && YInput != -1)
            {
                FiniteStateMachine.ChangeCurrentState(Player.MoveState);
            }
            else if (XInput == 0)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchIdleState);
            }
        }
    }
}
