using UnityEngine;

public abstract class IdleState : State
{
    private bool _flipAfterIdle;

    protected D_IdleState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected bool IsIdleTimeOver;
    protected float IdleTime;

    public IdleState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_IdleState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(0.0f);
        IsIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
        {
            Entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time > StartTime + IdleTime)
        {
            IsIdleTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Entity.CheckIfTouchingWall();
        IsDetectingLedge = Entity.CheckIfTouchingLedge();

        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();
    }

    public void ShouldFlipAfterIdle(bool flipAfterIdle) => _flipAfterIdle = flipAfterIdle;

    private void SetRandomIdleTime() => IdleTime = Random.Range(StateData.minimumIdleTime, StateData.maximumIdleTime);
}
