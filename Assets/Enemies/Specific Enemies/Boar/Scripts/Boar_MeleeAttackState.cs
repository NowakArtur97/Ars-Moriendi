using UnityEngine;

public class Boar_MeleeAttackState : MeleeAttackState
{
    private Boar _boar;

    public Boar_MeleeAttackState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, Boar boar) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange)
        {
            FinishAttack();
        }
        else
        {
            FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
        }
    }
}
