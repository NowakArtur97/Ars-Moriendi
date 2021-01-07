using UnityEngine;

public abstract class SlowDownState : EnemyState
{
    protected D_SlowDownState StateData;

    protected float CurrentVelocity;

    protected bool HasStopped;
    protected bool IsMinSlideTimeOver;

    protected bool IsDetectingWall;
    protected bool isDetectingLedge;

    public SlowDownState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_SlowDownState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsMinSlideTimeOver = false;
        HasStopped = false;
        CurrentVelocity = Enemy.MyRigidbody2D.velocity.x;
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

        IsDetectingWall = Enemy.CheckIfTouchingWall();
        isDetectingLedge = Enemy.CheckIfTouchingLedge();
    }

    private void SlowDown()
    {
        CurrentVelocity = Mathf.Abs(CurrentVelocity - StateData.decelerationSpeed);
        Enemy.SetVelocity(CurrentVelocity);
    }
}
