public class GoblinArcher_LookForPlayerState : LookForPlayerState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_LookForPlayerState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_LookForPlayerState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_goblinArcher.PlayerDetectedState);
        }
        else if (AreAllTurnsTimeDone)
        {
            FiniteStateMachine.ChangeState(_goblinArcher.MoveState);
        }
    }
}
