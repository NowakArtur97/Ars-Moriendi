public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (XInput != 0)
        {
            FiniteStateMachine.ChangeState(Player.MoveState);
        }
        else if (IsAnimationFinished)
        {
            FiniteStateMachine.ChangeState(Player.IdleState);
        }
    }
}
