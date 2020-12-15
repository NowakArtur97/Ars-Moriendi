using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool shouldTurnImmediately;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected int amountOfTurnsDone;
    protected float lastTurnTime;

    protected bool areAllTurnsDone;
    protected bool areAllTurnsTimeDone;

    public LookForPlayerState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_LookForPlayerState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0.0f);

        areAllTurnsDone = false;
        areAllTurnsTimeDone = false;
        amountOfTurnsDone = 0;
        lastTurnTime = startTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldTurnImmediately)
        {
            entity.Flip();
            amountOfTurnsDone++;
            lastTurnTime = Time.time;
            shouldTurnImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !areAllTurnsDone)
        {
            entity.Flip();
            amountOfTurnsDone++;
            lastTurnTime = Time.time;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            areAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && areAllTurnsDone)
        {
            areAllTurnsTimeDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = entity.CheckIfPlayerInMaxAgro();
    }

    public void SetShouldTurnImmediately(bool shouldTurnImmediately)
    {
        this.shouldTurnImmediately = shouldTurnImmediately;
    }
}
