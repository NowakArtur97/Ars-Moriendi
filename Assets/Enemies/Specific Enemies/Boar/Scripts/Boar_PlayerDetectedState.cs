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

        if (!IsDetectingLedge || IsDetectingWall)
        {
            Entity.Flip();
            FiniteStateMachine.ChangeState(boar.moveState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.lookForPlayerState);
        }
        else if (IsPlayerInMinAgroRange && ShouldPerformCloseRangeAction)
        {
            FiniteStateMachine.ChangeState(boar.meleeAttackState);
        }
        else if (IsPlayerInMaxAgroRange && ShouldPerformLongRangeAction)
        {
            FiniteStateMachine.ChangeState(boar.chargeState);
        }
    }
}
