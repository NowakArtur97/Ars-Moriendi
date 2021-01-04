public class PlayerJumpState : PlayerAbilityState
{
    private D_PlayerJumpState _jumpStateData;
    private int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerJumpState jumpStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _jumpStateData = jumpStateData;
        _amountOfJumpsLeft = _jumpStateData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityY(_jumpStateData.jumpVelocity);
        Player.InAirState.SetIsJumping();
        Player.InputHandler.UseJumpInput();

        DecreaseAmountOfJumps();

        IsAbilityDone = true;
    }

    public override bool CanUseAbility() => _amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = _jumpStateData.amountOfJumps;

    public void DecreaseAmountOfJumps() => _amountOfJumpsLeft--;
}
