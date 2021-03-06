﻿public class PlayerCrouchIdleState : PlayerGroundedState
{
    private D_PlayerCrouchIdleState _crouchIdleStateData;

    private bool _isTouchingCeiling;

    public PlayerCrouchIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        D_PlayerCrouchIdleState crouchIdleStateData) : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _crouchIdleStateData = crouchIdleStateData;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityZero();
        Player.SetBoxColliderHeight(_crouchIdleStateData.crouchColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (!CrouchInput && YInput != -1 && !_isTouchingCeiling)
            {
                FiniteStateMachine.ChangeCurrentState(Player.IdleState);
            }
            else if (XInput != 0)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchMoveState);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isTouchingCeiling = Player.CheckIfTouchingCeiling();
    }
}
