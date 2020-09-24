public class GoblinArcher_MoveState : MoveState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
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

        if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
        {
            finiteStateMachine.ChangeState(goblinArcher.playerDetectedState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            goblinArcher.idleState.SetFlipAfterIdle(true);
            finiteStateMachine.ChangeState(goblinArcher.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
