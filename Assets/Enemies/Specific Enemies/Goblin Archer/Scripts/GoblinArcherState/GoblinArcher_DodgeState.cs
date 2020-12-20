public class GoblinArcher_DodgeState : DodgeState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_DodgeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DodgeState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isDodgeTimeOver)
        {
            if (isPlayerInMinAgroRange && shouldPerformCloseRangeAction)
            {
                FiniteStateMachine.ChangeState(goblinArcher.meleeAttackState);
            }
            else if (shouldPerformLongRangeAction && isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(goblinArcher.rangedAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
            }
            else if (isPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(goblinArcher.playerDetectedState);
            }
        }
    }
}
