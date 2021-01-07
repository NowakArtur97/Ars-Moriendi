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
            if (isPlayerInMaxAgroRange)
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
