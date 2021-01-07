using UnityEngine;

public abstract class PlayerDetectedState : EnemyState
{
    protected D_PlayerDetectedState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingWallBehind;
    protected bool IsDetectingLedge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected bool ShouldPerformCloseRangeAction;
    protected bool ShouldPerformLongRangeAction;

    public PlayerDetectedState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_PlayerDetectedState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocity(0.0f);

        ShouldPerformCloseRangeAction = false;
        ShouldPerformLongRangeAction = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.timeForCloseRangeAction)
        {
            ShouldPerformCloseRangeAction = true;
        }

        if (Time.time >= StartTime + StateData.timeForLongRangeAction)
        {
            ShouldPerformLongRangeAction = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsDetectingWall = Enemy.CheckIfTouchingWall();
        IsDetectingWallBehind = Enemy.CheckIfBackIsTouchingWall();
        IsDetectingLedge = Enemy.CheckIfTouchingLedge();

        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();
    }
}
