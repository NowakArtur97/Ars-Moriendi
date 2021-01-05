public class JumpingMoveState : State
{
    protected D_JumpingMoveState StateData;

    protected bool IsGrounded;
    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;

    protected bool IsJumpOver;

    public JumpingMoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_JumpingMoveState stateData)
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

        IsGrounded = Entity.CheckIfGrounded();
        IsDetectingWall = Entity.CheckWall();
        IsDetectingLedge = Entity.CheckLedge();
    }
}
