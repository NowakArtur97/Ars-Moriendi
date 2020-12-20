public class GoblinArcher_StunState : StunState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isStunTimeOver)
        {
            if (shouldPerformCloseRangeAction && isPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(goblinArcher.meleeAttackState);
            }
            else
            {
                goblinArcher.lookForPlayerState.SetShouldTurnImmediately(true);
                FiniteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
            }
        }
    }
}