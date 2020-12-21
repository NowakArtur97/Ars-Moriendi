public class Boar_StunState : StunState
{
    private Boar _boar;

    public Boar_StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsStunTimeOver)
        {
            if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(_boar.MeleeAttackState);
            }
            else if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_boar.ChargeState);
            }
            else
            {
                _boar.LookForPlayerState.SetShouldTurnImmediately(true);
                FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
            }
        }
    }
}
