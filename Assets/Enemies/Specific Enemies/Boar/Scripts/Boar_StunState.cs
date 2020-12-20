public class Boar_StunState : StunState
{
    private Boar boar;

    public Boar_StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isStunTimeOver)
        {
            if (shouldPerformCloseRangeAction && isPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(boar.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(boar.chargeState);
            }
            else
            {
                boar.lookForPlayerState.SetShouldTurnImmediately(true);
                FiniteStateMachine.ChangeState(boar.lookForPlayerState);
            }
        }
    }
}
