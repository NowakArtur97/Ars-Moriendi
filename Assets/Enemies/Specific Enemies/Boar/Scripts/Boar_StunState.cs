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

        if (IsStunTimeOver)
        {
            if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(boar.meleeAttackState);
            }
            else if (IsPlayerInMaxAgroRange)
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
