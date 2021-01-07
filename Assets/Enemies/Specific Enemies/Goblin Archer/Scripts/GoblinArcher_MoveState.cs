public class GoblinArcher_MoveState : MoveState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_MoveState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_MoveState stateData, GoblinArcher goblinArcher)
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
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            _goblinArcher.IdleState.ShouldFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(_goblinArcher.IdleState);
        }
    }
}
