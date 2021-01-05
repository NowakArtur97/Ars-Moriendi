using UnityEngine;

public abstract class IdleState : State
{
    protected D_IdleState StateData;

    protected bool FlipAfterIdle;
    protected bool IsIdleTimeOver;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

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

        if (FlipAfterIdle)
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

        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();
    }

    public void SetFlipAfterIdle(bool flipAfterIdle) => FlipAfterIdle = flipAfterIdle;

    private void SetRandomIdleTime() => IdleTime = Random.Range(StateData.minimumIdleTime, StateData.maximumIdleTime);
}
