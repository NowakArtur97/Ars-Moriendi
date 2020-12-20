public class Boar_ChargeState : ChargeState
{
    private Boar boar;

    public Boar_ChargeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_ChargeState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldPerformCloseRangeAction)
        {
            FiniteStateMachine.ChangeState(boar.meleeAttackState);
        }
        else if (HasDetectedObstacle())
        {
            FiniteStateMachine.ChangeState(boar.lookForPlayerState);
        }
        else if (isDetectingPlayerAbove)
        {
            FiniteStateMachine.ChangeState(boar.slowDownState);
        }
        else if (isPlayerInMinAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.playerDetectedState);
        }
    }

    private bool HasDetectedObstacle()
    {
        return !isDetectingLedge || isDetectingWall;
    }
}
