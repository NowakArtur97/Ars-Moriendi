﻿using UnityEngine;

public class Slime_MeleeAttackState : MeleeAttackState
{
    private Slime _slime;

    public Slime_MeleeAttackState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, Slime slime) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_slime.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_slime.IdleState);
            }
        }
    }
}
