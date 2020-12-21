public class Boar_IdleState : IdleState
{
    private Boar _boar;

    public Boar_IdleState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_IdleState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
        }
        else if (IsIdleTimeOver)
        {
            FiniteStateMachine.ChangeState(_boar.MoveState);
        }
    }
}
