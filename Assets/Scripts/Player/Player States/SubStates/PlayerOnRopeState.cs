using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState : PlayerAbilityState
{
    protected bool RopeInput;
    protected bool RopeInputStop;
    protected bool IsAiming;
    protected bool RopeAttached;
    protected bool IsHoldingRope;
    protected Vector2 PlayerPosition;
    protected Vector2 AimDirection;
    protected List<Vector2> RopePositions;

    public PlayerOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
        RopePositions = new List<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        RopeInput = Player.InputHandler.SecondaryAttackInput;
        RopeInputStop = Player.InputHandler.SecondaryAttackInputStop;

        if (!IsExitingState)
        {
            if (!RopeAttached && IsAiming && !IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateAim);
            }
            else if (RopeAttached && !IsAiming && !IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateAttach);
            }
            else if (RopeAttached && !IsAiming && IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateMove);
            }
            else if (!RopeAttached && !IsAiming && !IsHoldingRope)
            {
                Player.InputHandler.UseSecondaryAttackInputStop();

                IsAbilityDone = true;
            }
        }
    }
}
