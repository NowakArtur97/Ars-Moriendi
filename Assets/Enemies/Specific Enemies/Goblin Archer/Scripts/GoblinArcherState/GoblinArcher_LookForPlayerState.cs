public class GoblinArcher_LookForPlayerState : LookForPlayerState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_LookForPlayerState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_LookForPlayerState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(goblinArcher.playerDetectedState);
        }
        else if (areAllTurnsTimeDone)
        {
            FiniteStateMachine.ChangeState(goblinArcher.moveState);
        }
    }
}
