public class FiniteStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
