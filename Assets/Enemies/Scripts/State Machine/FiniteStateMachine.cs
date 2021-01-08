﻿public class FiniteStateMachine
{
    public EnemyState CurrentState { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
