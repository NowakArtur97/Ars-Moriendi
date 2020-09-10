using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool shouldPerformLongRangeAction;
    protected bool shouldPerformShortRangeAction;

    public PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0.0f);

        shouldPerformShortRangeAction = false;
        shouldPerformLongRangeAction = false;

        DoChecks();
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
            shouldPerformShortRangeAction = true;
        }

        if (Time.time >= startTime + stateData.timeForLongRangeAction)
        {
            shouldPerformLongRangeAction = true;
        }
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();

        DoChecks();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = entity.CheckIfPlayerInMaxAgro();
    }
}
