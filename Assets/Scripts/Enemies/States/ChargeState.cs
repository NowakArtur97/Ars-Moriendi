public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isDetectingPlayerAbove;

    protected bool isPlayerInMinAgroRange;

    protected bool shouldPerformCloseRangeAction;
    protected bool shouldPerformLongRangeAction;

    public ChargeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_ChargeState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isPlayerInMinAgroRange = entity.CheckIfPlayerInMinAgro();
        isDetectingPlayerAbove = entity.CheckIfPlayerJumpedOver();

        shouldPerformCloseRangeAction = entity.CheckIfPlayerInCloseRangeAction();
        shouldPerformLongRangeAction = entity.CheckIfPlayerInLongRangeAction();
    }
}
