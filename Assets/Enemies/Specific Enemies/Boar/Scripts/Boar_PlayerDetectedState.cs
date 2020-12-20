public class Boar_PlayerDetectedState : PlayerDetectedState
{
    private Boar boar;

    public Boar_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isDetectingLedge || isDetectingWall)
        {
            Entity.Flip();
            FiniteStateMachine.ChangeState(boar.moveState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.lookForPlayerState);
        }
        else if (isPlayerInMinAgroRange && shouldPerformCloseRangeAction)
        {
            FiniteStateMachine.ChangeState(boar.meleeAttackState);
        }
        else if (isPlayerInMaxAgroRange && shouldPerformLongRangeAction)
        {
            FiniteStateMachine.ChangeState(boar.chargeState);
        }
    }
}
