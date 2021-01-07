public abstract class ChargeState : EnemyState
{
    protected D_ChargeState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;
    protected bool IsDetectingPlayerAbove;

    protected bool IsPlayerInMinAgroRange;

    protected bool ShouldPerformCloseRangeAction;
    protected bool ShouldPerformLongRangeAction;

    public ChargeState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_ChargeState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocity(StateData.chargeSpeed);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Enemy.CheckIfTouchingWall();
        IsDetectingLedge = Enemy.CheckIfTouchingLedge();
        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsDetectingPlayerAbove = Enemy.CheckIfPlayerJumpedOver();

        ShouldPerformCloseRangeAction = Enemy.CheckIfPlayerInCloseRangeAction();
        ShouldPerformLongRangeAction = Enemy.CheckIfPlayerInLongRangeAction();
    }
}
