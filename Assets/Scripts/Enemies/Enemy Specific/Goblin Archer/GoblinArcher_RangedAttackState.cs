using UnityEngine;

public class GoblinArcher_RangedAttackState : RangedAttackState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_RangedAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_RangedAttackState stateData, GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMaxAgroRange)
            {
                finiteStateMachine.ChangeState(goblinArcher.playerDetectedState);
            }
            else
            {
                finiteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
            }
        }
    }
}
