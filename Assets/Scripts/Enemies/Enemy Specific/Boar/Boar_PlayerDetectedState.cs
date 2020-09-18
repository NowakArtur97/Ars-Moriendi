public class Boar_PlayerDetectedState : PlayerDetectedState
{
    private Boar boar;

    public Boar_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData, Boar boar)
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

        if (!isPlayerInMaxAgroRange)
        {
            finiteStateMachine.ChangeState(boar.lookForPlayerState);
        }
        else if (isPlayerInMinAgroRange && shouldPerformCloseRangeAction)
        {
            finiteStateMachine.ChangeState(boar.meleeAttackState);
        }
        else if (isPlayerInMaxAgroRange && shouldPerformLongRangeAction)
        {
            finiteStateMachine.ChangeState(boar.chargeState);
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
