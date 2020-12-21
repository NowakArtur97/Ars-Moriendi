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

        if (IsPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.playerDetectedState);
        }
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            boar.idleState.SetFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(boar.idleState);
        }
    }
}
