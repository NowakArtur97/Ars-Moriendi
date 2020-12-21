using UnityEngine;

public abstract class LookForPlayerState : State
{
    protected D_LookForPlayerState StateData;

    protected bool ShouldTurnImmediately;

    protected bool isPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected int AmountOfTurnsDone;
    protected float LastTurnTime;

    protected bool AreAllTurnsDone;
    protected bool AreAllTurnsTimeDone;

    public LookForPlayerState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_LookForPlayerState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(0.0f);

        AreAllTurnsDone = false;
        AreAllTurnsTimeDone = false;
        AmountOfTurnsDone = 0;
        LastTurnTime = StartTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (ShouldTurnImmediately)
        {
            Entity.Flip();
            AmountOfTurnsDone++;
            LastTurnTime = Time.time;
            ShouldTurnImmediately = false;
        }
        else if (Time.time >= LastTurnTime + StateData.timeBetweenTurns && !AreAllTurnsDone)
        {
            Entity.Flip();
            AmountOfTurnsDone++;
            LastTurnTime = Time.time;
        }

        if (AmountOfTurnsDone >= StateData.amountOfTurns)
        {
            AreAllTurnsDone = true;
        }

        if (Time.time >= LastTurnTime + StateData.timeBetweenTurns && AreAllTurnsDone)
        {
            AreAllTurnsTimeDone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro;
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro;
    }

    public void SetShouldTurnImmediately(bool shouldTurnImmediately)
    {
        ShouldTurnImmediately = shouldTurnImmediately;
    }
}
