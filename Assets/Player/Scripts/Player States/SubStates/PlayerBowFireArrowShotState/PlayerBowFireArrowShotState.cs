﻿using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    protected D_PlayerBowArrowShotState PlayerFireArrowShotData;

    protected bool IsAiming;
    protected bool IsShooting;
    protected bool CanShot;
    protected float LastShotTime;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerBowArrowShotState playerFireArrowShotData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition)
    {
        PlayerFireArrowShotData = playerFireArrowShotData;
        CanShot = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (IsAiming)
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.FireArrowShotStateAim);
            }
            else if (IsShooting)
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.FireArrowShotStateFinish);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        IsShooting = false;
        IsAiming = false;
        CanShot = true;
    }

    public bool CheckIfCanShoot() => CanShot && Time.time >= LastShotTime + PlayerFireArrowShotData.bowShotCooldown;
}
