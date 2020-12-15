public class Boar_DeadState : DeadState
{
    private Boar boar;

    public Boar_DeadState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DeadState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }
}
