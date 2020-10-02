public class PlayerJumpState : PlayerAbilityState
{
    private int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
        _amountOfJumpsLeft = PlayerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityY(PlayerData.jumpVelocity);

        DecreaseAmountOfJumps();

        IsAbilityDone = true;
    }

    public bool CanJump() => _amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

    public void DecreaseAmountOfJumps() => _amountOfJumpsLeft--;

}
