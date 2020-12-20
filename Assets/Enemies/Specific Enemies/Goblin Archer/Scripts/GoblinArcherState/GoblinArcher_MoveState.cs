public class GoblinArcher_MoveState : MoveState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
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
        else if (!isDetectingLedge || isDetectingWall)
        {
            goblinArcher.idleState.SetFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(goblinArcher.idleState);
        }
    }
}
