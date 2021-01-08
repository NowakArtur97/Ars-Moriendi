public class Slime_DeadState : DeadState
{
    private Slime _slime;

    public Slime_DeadState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DeadState stateData, Slime slime)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _slime = slime;
    }
}
