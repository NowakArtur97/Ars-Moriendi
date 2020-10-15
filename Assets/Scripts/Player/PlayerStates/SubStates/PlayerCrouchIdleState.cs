public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(0.0f);
    }
}
