﻿public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime _slime;

    public Slime_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData, Slime slime)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
        {
            FiniteStateMachine.ChangeState(_slime.MeleeAttackState);
        }
        else if (!IsPlayerInMaxAgroRange)
        //else if (ShouldPerformLongRangeAction && IsPlayerInMaxAgroRange)
        {
            //TODO: SLIME Add ranged action
            //FiniteStateMachine.ChangeState(_slime.RangedAttackState);
            FiniteStateMachine.ChangeState(_slime.IdleState);
        }
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            FiniteStateMachine.ChangeState(_slime.JumpingMoveState);
        }
    }
}
