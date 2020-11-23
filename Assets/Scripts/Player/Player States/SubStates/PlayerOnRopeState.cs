using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState : PlayerAbilityState
{
    protected bool RopeInput;
    protected bool RopeInputStop;
    protected Vector2 PlayerPosition;
    protected Vector2 AimDirection;

    protected static bool IsAiming;
    protected static bool RopeAttached;
    protected static bool IsHoldingRope;
    protected static List<Vector2> RopePositions = new List<Vector2>();

    public PlayerOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        RopeInput = Player.InputHandler.SecondaryAttackInput;
        RopeInputStop = Player.InputHandler.SecondaryAttackInputStop;

        if (!IsExitingState)
        {
            if (!RopeAttached && !IsAiming && !IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateAttach);
            }
            else if (RopeAttached && !IsAiming && !IsHoldingRope)
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
