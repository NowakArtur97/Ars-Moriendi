﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState : PlayerAbilityState
{
    protected D_PlayerOnRopeState OnRopeStateData;

    protected bool IsGrounded;
    protected bool RopeInputStop;
    protected Vector2 PlayerPosition;

    protected static bool IsAiming;
    protected static bool RopeAttached;
    protected static bool IsHoldingRope;
    protected static Vector2 AimDirection;
    protected static List<Vector2> RopePositions = new List<Vector2>();
    protected static Dictionary<Vector2, int> WrapPointsLookup = new Dictionary<Vector2, int>();

    public PlayerOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerOnRopeState onRopeStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        OnRopeStateData = onRopeStateData;
    }

    public override void Enter()
    {
        base.Enter();

        SetAnimationBasedOnPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        SetAnimationBasedOnPosition();

        PlayerPosition = Player.transform.position;
        RopeInputStop = Player.InputHandler.SecondaryAttackInputStop;

        if (!IsExitingState)
        {
            if (!RopeAttached && !IsAiming && IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateAttach);
            }
            else if (RopeAttached && !IsAiming && IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateMove);
            }
            else if (!RopeAttached && !IsAiming && !IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateFinish);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
    }

    private void SetAnimationBasedOnPosition()
    {
        if (IsGrounded)
        {
            Player.MyAnmator.SetBool("inAir", false);
            Player.MyAnmator.SetBool("idle", true);
        }
        else
        {
            Player.MyAnmator.SetBool("idle", false);
            Player.MyAnmator.SetBool("inAir", true);
        }
    }
}