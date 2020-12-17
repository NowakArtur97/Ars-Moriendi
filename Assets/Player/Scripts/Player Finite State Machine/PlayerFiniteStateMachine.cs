public class PlayerFiniteStateMachine
{
    public PlayerState CurrentState { get; private set; }
    public PlayerState PrimaryAttackState { get; private set; }
    public PlayerState SecondaryAttackState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeCurrentState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ChangePrimaryAttackState(PlayerState newPrimaryAttackState)
    {
        PrimaryAttackState = newPrimaryAttackState;
    }

    public void ChangeSecondaryAttackState(PlayerState newSecondaryAttackState)
    {
        SecondaryAttackState = newSecondaryAttackState;
    }
}
