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

        DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldPerformCloseRangeAction)
        {
            finiteStateMachine.ChangeState(boar.meleeAttackState);
        }
        else if (HasDetectedObstacle())
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
