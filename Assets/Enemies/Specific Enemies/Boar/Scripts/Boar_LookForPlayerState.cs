public class Boar_LookForPlayerState : LookForPlayerState
{
    private Boar _boar;

    public Boar_LookForPlayerState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_LookForPlayerState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
        }
        else if (AreAllTurnsTimeDone)
        {
            FiniteStateMachine.ChangeState(_boar.MoveState);
        }
    }
}
