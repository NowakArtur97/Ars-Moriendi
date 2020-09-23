public class GoblinArcher_PlayerDetectedState : PlayerDetectedState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
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

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();

        if (!isDetectingLedge || isDetectingWall)
        {
            entity.Flip();
            finiteStateMachine.ChangeState(goblinArcher.moveState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            finiteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
        }
        else if (isPlayerInMinAgroRange && shouldPerformCloseRangeAction)
        {
            finiteStateMachine.ChangeState(goblinArcher.meleeAttackState);
        }
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
