using UnityEngine;

public class GoblinArcher_RangedAttackState : RangedAttackState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_RangedAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_RangedAttackState stateData, GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_goblinArcher.LookForPlayerState);
            }
        }
    }
}
