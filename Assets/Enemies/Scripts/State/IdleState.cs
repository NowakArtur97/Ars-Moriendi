using UnityEngine;

public abstract class IdleState : EnemyState
{
    private bool _flipAfterIdle;

    protected D_IdleState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected bool IsIdleTimeOver;
    protected float IdleTime;

    public IdleState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_IdleState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocity(0.0f);
        IsIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
        {
            Enemy.Flip();
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

        IsDetectingWall = Enemy.CheckIfTouchingWall();
        IsDetectingLedge = Enemy.CheckIfTouchingLedge();

        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();
    }

    public void ShouldFlipAfterIdle(bool flipAfterIdle) => _flipAfterIdle = flipAfterIdle;

    private void SetRandomIdleTime() => IdleTime = Random.Range(StateData.minimumIdleTime, StateData.maximumIdleTime);
}
