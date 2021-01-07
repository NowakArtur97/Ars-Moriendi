public abstract class ChargeState : State
{
    protected D_ChargeState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;
    protected bool IsDetectingPlayerAbove;

    protected bool IsPlayerInMinAgroRange;

    protected bool ShouldPerformCloseRangeAction;
    protected bool ShouldPerformLongRangeAction;

    public ChargeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_ChargeState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(StateData.chargeSpeed);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.CheckIfTouchingWall();
        IsDetectingLedge = Entity.CheckIfTouchingLedge();
        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        IsDetectingPlayerAbove = Entity.CheckIfPlayerJumpedOver();

        ShouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        ShouldPerformLongRangeAction = Entity.CheckIfPlayerInLongRangeAction();
    }
}
