public class GoblinArcher_DeadState : DeadState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_DeadState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DeadState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.goblinArcher = goblinArcher;
    }
}

