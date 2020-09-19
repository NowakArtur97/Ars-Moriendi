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

        entity.SetVelocity(0.0f);

        shouldPerformCloseRangeAction = false;
        shouldPerformLongRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();

        if (Time.time >= startTime + stateData.timeForShortRangeAction)
        {
            shouldPerformCloseRangeAction = true;
        }

        if (Time.time >= startTime + stateData.timeForLongRangeAction)
        {
            shouldPerformLongRangeAction = true;
        }
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();

        isPlayerInMinAgroRange = entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = entity.CheckIfPlayerInMaxAgro();

        shouldPerformCloseRangeAction = entity.CheckIfPlayerInCloseRangeAction();
        shouldPerformLongRangeAction = entity.CheckIfPlayerInLongRangeAction();
    }
}
