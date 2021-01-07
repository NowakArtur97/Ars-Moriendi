using UnityEngine;

public abstract class PlayerDetectedState : State
{
    protected D_PlayerDetectedState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingWallBehind;
    protected bool IsDetectingLedge;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected bool ShouldPerformCloseRangeAction;
    protected bool ShouldPerformLongRangeAction;

    public PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(0.0f);

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

        IsDetectingWall = Entity.CheckIfTouchingWall();
        IsDetectingWallBehind = Entity.CheckIfBackIsTouchingWall();
        IsDetectingLedge = Entity.CheckIfTouchingLedge();

        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();
    }
}
