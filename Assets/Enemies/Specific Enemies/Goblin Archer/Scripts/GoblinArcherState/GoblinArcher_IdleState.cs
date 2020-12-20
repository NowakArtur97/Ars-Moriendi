public class GoblinArcher_IdleState : IdleState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_IdleState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_IdleState stateData, GoblinArcher goblinArcher)
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
        else if (isIdleTimeOver)
        {
            FiniteStateMachine.ChangeState(goblinArcher.moveState);
        }
    }
}
