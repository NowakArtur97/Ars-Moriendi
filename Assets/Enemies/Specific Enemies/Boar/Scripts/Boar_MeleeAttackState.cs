using UnityEngine;

public class Boar_MeleeAttackState : MeleeAttackState
{
    private Boar boar;

    public Boar_MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, Boar boar) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange)
        {
            FinishAttack();
            FiniteStateMachine.ChangeState(boar.playerDetectedState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.lookForPlayerState);
        }
    }
}
