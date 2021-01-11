public class GoblinArcher_DamageState : DamageState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_DamageState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DamageState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (_goblinArcher.StatsManager.IsStunned && FiniteStateMachine.CurrentState != _goblinArcher.StunState)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.StunState);
            }
            else if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.PlayerDetectedState);
            }
            else
            {
                _goblinArcher.LookForPlayerState.SetShouldTurnImmediately(_goblinArcher.LastDamageDirection == _goblinArcher.FacingDirection);
                FiniteStateMachine.ChangeState(_goblinArcher.LookForPlayerState);
            }
        }
    }
}
