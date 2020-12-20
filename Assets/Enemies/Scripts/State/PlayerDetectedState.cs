using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool shouldPerformCloseRangeAction;
    protected bool shouldPerformLongRangeAction;

    public PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(0.0f);

        shouldPerformCloseRangeAction = false;
        shouldPerformLongRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.timeForShortRangeAction)
        {
            shouldPerformCloseRangeAction = true;
        }

        if (Time.time >= startTime + stateData.timeForLongRangeAction)
        {
            shouldPerformLongRangeAction = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = Entity.CheckWall();
        isDetectingLedge = Entity.CheckLedge();

        isPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();

        shouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        shouldPerformLongRangeAction = Entity.CheckIfPlayerInLongRangeAction();
    }
}
