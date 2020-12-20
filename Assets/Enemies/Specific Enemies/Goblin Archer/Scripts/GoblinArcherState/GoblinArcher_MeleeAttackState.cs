using UnityEngine;

public class GoblinArcher_MeleeAttackState : MeleeAttackState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(goblinArcher.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
            }
        }
    }
}
