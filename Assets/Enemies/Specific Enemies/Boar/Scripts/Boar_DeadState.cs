public class Boar_DeadState : DeadState
{
    private Boar _boar;

    public Boar_DeadState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_DeadState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this._boar = boar;
    }
}
