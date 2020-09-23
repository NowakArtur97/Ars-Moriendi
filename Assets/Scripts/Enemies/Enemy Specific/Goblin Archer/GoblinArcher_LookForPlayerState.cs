public class GoblinArcher_LookForPlayerState : LookForPlayerState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_LookForPlayerState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_LookForPlayerState stateData,
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

        if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
        {
            finiteStateMachine.ChangeState(goblinArcher.playerDetectedState);
        }
        else if (areAllTurnsTimeDone)
        {
            finiteStateMachine.ChangeState(goblinArcher.moveState);
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
