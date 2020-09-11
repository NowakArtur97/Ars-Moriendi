using UnityEngine;

public class SlowDownState : State
{
    protected D_SlowDownState stateData;

    protected float currentVelocity;
    protected float slideTime;

    protected bool hasStopped;
    protected bool isMinSlideTimeOver;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    public SlowDownState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_SlowDownState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isMinSlideTimeOver = false;
        hasStopped = false;
        currentVelocity = entity.myRigidbody2D.velocity.x;

        DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();

        if (Time.time >= startTime + stateData.minSlideTime)
        {
            isMinSlideTimeOver = true;
        }

        if (currentVelocity <= 0.1f)
        {
            hasStopped = true;
        }
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();

        DoChecks();

        if (!hasStopped)
        {
            SlowDown();
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }

    private void SlowDown()
    {
        currentVelocity = Mathf.Abs(currentVelocity - stateData.decelerationSpeed);
        entity.SetVelocity(currentVelocity);
    }
}
