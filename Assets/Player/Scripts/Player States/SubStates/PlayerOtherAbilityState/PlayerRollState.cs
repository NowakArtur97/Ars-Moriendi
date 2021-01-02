public class PlayerRollState : PlayerAbilityState
{
    private D_PlayerRollState _rollStateData;

    public PlayerRollState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerRollState rollStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _rollStateData = rollStateData;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(Player.FacingDirection * _rollStateData.rollVelocity);
        // TODO: Remove?
        Player.SetBoxColliderHeight(_rollStateData.rollColliderHeight);

        Player.StatsManager.SetIsRolling(true);
    }

    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            IsAbilityDone = true;
        }

        base.LogicUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        Player.SetVelocityZero();

        Player.StatsManager.SetIsRolling(false);
    }
}
