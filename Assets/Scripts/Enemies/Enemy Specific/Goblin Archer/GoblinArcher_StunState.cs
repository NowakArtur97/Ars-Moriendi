public class GoblinArcher_StunState : StunState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData,
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

        if (isStunTimeOver)
        {
            if (shouldPerformCloseRangeAction && isPlayerInMinAgroRange)
            {
                finiteStateMachine.ChangeState(goblinArcher.meleeAttackState);
            }
            else
            {
                goblinArcher.lookForPlayerState.SetShouldTurnImmediately(true);
                finiteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
            }
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