using UnityEngine;

public class GoblinArcher_MeleeAttackState : MeleeAttackState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (IsPlayerInMinAgroRange)
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
