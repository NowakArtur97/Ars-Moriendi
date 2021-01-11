public class Slime_DamageState : DamageState
{
    private Slime _slime;

    public Slime_DamageState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DamageState stateData, Slime slime)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (_slime.StatsManager.IsStunned && FiniteStateMachine.CurrentState != _slime.StunState)
            {
                FiniteStateMachine.ChangeState(_slime.StunState);
            }
            else if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_slime.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_slime.IdleState);
            }
        }
    }
}
