using UnityEngine;

public class Slime_AreaAttackState : AreaAttackState
{
    private Slime _slime;

    public Slime_AreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition, D_AreaAttackState stateData,
        Slime slime) : base(finiteStateMachine, enemy, animationBoolName, attackPosition, stateData)
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
