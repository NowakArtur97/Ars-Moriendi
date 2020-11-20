public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
        _amountOfJumpsLeft = PlayerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityY(PlayerData.jumpVelocity);
        Player.InAirState.SetIsJumping();
        Player.InputHandler.UseJumpInput();

        DecreaseAmountOfJumps();

        IsAbilityDone = true;
    }

    public bool CanJump() => _amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

    public void DecreaseAmountOfJumps() => _amountOfJumpsLeft--;
}
