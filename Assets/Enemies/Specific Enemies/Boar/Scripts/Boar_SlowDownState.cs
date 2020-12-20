public class Boar_SlowDownState : SlowDownState
{
    private Boar boar;

    public Boar_SlowDownState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_SlowDownState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isDetectingLedge || isDetectingWall)
        {
            boar.idleState.SetFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(boar.idleState);
        }
        else if (hasStopped && isMinSlideTimeOver)
        {
            boar.lookForPlayerState.SetShouldTurnImmediately(true);
            FiniteStateMachine.ChangeState(boar.lookForPlayerState);
        }
    }
}
