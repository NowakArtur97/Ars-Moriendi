public class Boar_SlowDownState : SlowDownState
{
    private Boar _boar;

    public Boar_SlowDownState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_SlowDownState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isDetectingLedge || IsDetectingWall)
        {
            _boar.IdleState.ShouldFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(_boar.IdleState);
        }
        else if (HasStopped && IsMinSlideTimeOver)
        {
            _boar.LookForPlayerState.SetShouldTurnImmediately(true);
            FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
        }
    }
}
