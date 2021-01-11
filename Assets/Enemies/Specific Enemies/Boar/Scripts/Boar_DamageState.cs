public class Boar_DamageState : DamageState
{
    private Boar _boar;

    public Boar_DamageState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DamageState stateData, Boar boar)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (_boar.StatsManager.IsStunned && FiniteStateMachine.CurrentState != _boar.StunState)
            {
                FiniteStateMachine.ChangeState(_boar.StunState);
            }
            else if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
            }
            else
            {
                _boar.LookForPlayerState.SetShouldTurnImmediately(_boar.LastDamageDirection == _boar.FacingDirection);
                FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
            }
        }
    }
}
