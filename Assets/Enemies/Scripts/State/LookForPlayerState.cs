using UnityEngine;

public abstract class LookForPlayerState : EnemyState
{
    protected D_LookForPlayerState StateData;

    protected bool ShouldTurnImmediately;

    protected bool isPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    protected int AmountOfTurnsDone;
    protected float LastTurnTime;

    protected bool AreAllTurnsDone;
    protected bool AreAllTurnsTimeDone;

    public LookForPlayerState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_LookForPlayerState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.SetVelocity(0.0f);

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
            Enemy.Flip();
            AmountOfTurnsDone++;
            LastTurnTime = Time.time;
            ShouldTurnImmediately = false;
        }
        else if (Time.time >= LastTurnTime + StateData.timeBetweenTurns && !AreAllTurnsDone)
        {
            Enemy.Flip();
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

        isPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();
    }

    public void SetShouldTurnImmediately(bool shouldTurnImmediately) => ShouldTurnImmediately = shouldTurnImmediately;
}
