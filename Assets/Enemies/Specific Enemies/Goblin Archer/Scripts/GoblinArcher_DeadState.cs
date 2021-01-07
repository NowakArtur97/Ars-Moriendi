public class GoblinArcher_DeadState : DeadState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_DeadState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_DeadState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }
}

