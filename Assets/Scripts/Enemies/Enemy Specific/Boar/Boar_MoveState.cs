public class Boar_MoveState : MoveState
{
    private Boar boar;

    public Boar_MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData, Boar boar) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();

        if (isPlayerInMinAgroRange)
        {
            finiteStateMachine.ChangeState(boar.playerDetectedState);
        }
        else if (ShouldFlip())
        {
            boar.idleState.SetFlipAfterIdle(true);
            finiteStateMachine.ChangeState(boar.idleState);
        }
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();
    }

    private bool ShouldFlip()
    {
        return !isDetectingLedge || isDetectingWall;
    }
}
