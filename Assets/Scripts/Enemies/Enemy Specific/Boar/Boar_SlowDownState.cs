public class Boar_SlowDownState : SlowDownState
{
    private Boar boar;

    public Boar_SlowDownState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_SlowDownState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
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
            boar.idleState.SetFlipAfterIdle(true);
            finiteStateMachine.ChangeState(boar.idleState);
        }
        else if (hasStopped && isMinSlideTimeOver)
        {
            boar.lookForPlayerState.SetShouldTurnImmediately(true);
            finiteStateMachine.ChangeState(boar.lookForPlayerState);
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
