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

        Entity.SetVelocity(stateData.chargeSpeed);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = Entity.CheckWall();
        isDetectingLedge = Entity.CheckLedge();
        isPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        isDetectingPlayerAbove = Entity.CheckIfPlayerJumpedOver();

        shouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        shouldPerformLongRangeAction = Entity.CheckIfPlayerInLongRangeAction();
    }
}
