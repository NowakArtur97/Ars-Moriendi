public class Boar_StunState : StunState
{
    private Boar boar;

    public Boar_StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData, Boar boar)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isStunTimeOver)
        {
            if (shouldPerformCloseRangeAction && isPlayerInMinAgroRange)
            {
                finiteStateMachine.ChangeState(boar.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange)
            {
                finiteStateMachine.ChangeState(boar.chargeState);
            }
            else
            {
                boar.lookForPlayerState.SetShouldTurnImmediately(true);
                finiteStateMachine.ChangeState(boar.lookForPlayerState);
            }
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
