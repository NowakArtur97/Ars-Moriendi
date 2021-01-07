public class JumpingMoveState : State
{
    protected D_JumpingMoveState StateData;

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

        Entity.SetVelocity(StateData.jumpingMovementSpeed, StateData.jumpingAngle, Entity.FacingDirection);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.CheckIfTouchingWall();
        IsDetectingLedge = Entity.CheckIfTouchingLedge();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Entity.SetVelocity(0.0f);
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAnimationFinished = true;
    }
}
