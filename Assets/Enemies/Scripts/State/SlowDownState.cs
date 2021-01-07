using UnityEngine;

public abstract class SlowDownState : State
{
    protected D_SlowDownState StateData;

    protected float CurrentVelocity;

    protected bool HasStopped;
    protected bool IsMinSlideTimeOver;

    protected bool IsDetectingWall;
    protected bool isDetectingLedge;

    public SlowDownState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_SlowDownState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsMinSlideTimeOver = false;
        HasStopped = false;
        CurrentVelocity = Entity.MyRigidbody2D.velocity.x;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.minSlideTime)
        {
            IsMinSlideTimeOver = true;
        }

        if (CurrentVelocity <= 0.1f)
        {
            HasStopped = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!HasStopped)
        {
            SlowDown();
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.CheckIfTouchingWall();
        isDetectingLedge = Entity.CheckIfTouchingLedge();
    }

    private void SlowDown()
    {
        CurrentVelocity = Mathf.Abs(CurrentVelocity - StateData.decelerationSpeed);
        Entity.SetVelocity(CurrentVelocity);
    }
}
