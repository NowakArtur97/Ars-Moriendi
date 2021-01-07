public abstract class MoveState : State
{
    protected D_MoveState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(StateData.movementSpeed);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.CheckIfTouchingWall();
        IsDetectingLedge = Entity.CheckIfTouchingLedge();
        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();
    }
}
