public class GoblinArcher_StunState : StunState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_StunState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_StunState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsStunTimeOver)
        {
            if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_goblinArcher.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_goblinArcher.LookForPlayerState);
            }
        }
    }
}