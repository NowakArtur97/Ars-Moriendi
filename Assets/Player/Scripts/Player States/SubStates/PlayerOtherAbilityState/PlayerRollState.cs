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

        Player.SetBoxColliderHeight(_rollStateData.rollColliderHeight);

        Player.SetVelocityX(Player.FacingDirection * _rollStateData.rollVelocity);

        Player.StatsManager.SetIsRolling(true);
    }

    public override void Exit()
    {
        base.Exit();

        Player.StatsManager.SetIsRolling(false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        Player.MyAnmator.SetBool("isTouchingCeiling", IsTouchingCeiling);

        if (IsTouchingCeiling)
        {
            IsAbilityDone = true;
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAbilityDone = true;
    }
}
