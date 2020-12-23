public class GoblinArcher_DodgeState : DodgeState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_DodgeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DodgeState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsDodgeTimeOver)
        {
            if (IsPlayerInMinAgroRange && ShouldPerformCloseRangeAction)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.MeleeAttackState);
            }
            else if (ShouldPerformLongRangeAction && isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.RangedAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.LookForPlayerState);
            }
            else if (isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.PlayerDetectedState);
            }
        }
    }
}
