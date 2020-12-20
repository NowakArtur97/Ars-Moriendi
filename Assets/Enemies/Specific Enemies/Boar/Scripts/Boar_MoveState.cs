public class Boar_MoveState : MoveState
{
    private Boar boar;

    public Boar_MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData, Boar boar) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.playerDetectedState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            boar.idleState.SetFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(boar.idleState);
        }
    }
}
