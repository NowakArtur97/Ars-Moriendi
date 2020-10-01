public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityY(PlayerData.jumpVelocity);

        IsAbilityDone = true;
    }
}
