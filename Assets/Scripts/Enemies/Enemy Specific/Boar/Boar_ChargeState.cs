public class Boar_ChargeState : ChargeState
{
    private Boar boar;

    public Boar_ChargeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_ChargeState stateData, Boar boar)
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

        if (HasDetectedObstacle())
        {
            finiteStateMachine.ChangeState(boar.lookForPlayerState);
        }
        else if (isDetectingPlayerAbove)
        {
            finiteStateMachine.ChangeState(boar.slowDownState);
        }
        else if (isPlayerInMinAgroRange)
        {
            finiteStateMachine.ChangeState(boar.playerDetectedState);
        }

        // TODO: Add attack state
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    private bool HasDetectedObstacle()
    {
        return !isDetectingLedge || isDetectingWall;
    }
}
