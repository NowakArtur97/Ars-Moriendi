public class GoblinArcher_IdleState : IdleState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_IdleState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_IdleState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_goblinArcher.PlayerDetectedState);
        }
        else if (IsIdleTimeOver)
        {
            FiniteStateMachine.ChangeState(_goblinArcher.MoveState);
        }
    }
}
