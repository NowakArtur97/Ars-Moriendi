using UnityEngine;

public class GoblinArcher_MeleeAttackState : MeleeAttackState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                finiteStateMachine.ChangeState(goblinArcher.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                finiteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        DoChecks();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
