public class GoblinArcher_StunState : StunState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsStunTimeOver)
        {
            if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.MeleeAttackState);
            }
            else
            {
                _goblinArcher.LookForPlayerState.SetShouldTurnImmediately(true);
                FiniteStateMachine.ChangeState(_goblinArcher.LookForPlayerState);
            }
        }
    }
}