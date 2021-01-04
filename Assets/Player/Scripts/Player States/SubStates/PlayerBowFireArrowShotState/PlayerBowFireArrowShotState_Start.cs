using UnityEngine;

public class PlayerBowFireArrowShotState_Start : PlayerBowFireArrowShotState
{
    private bool _isAiming;
    private float _lastShotTime;

    public PlayerBowFireArrowShotState_Start(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition, D_PlayerBowArrowShotState playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(0.0f);

        _isAiming = false;
        _lastShotTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAiming)
        {
            Player.FiniteStateMachine.ChangeCurrentState(Player.FireArrowShotStateAim);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isAiming = true;
    }

    public override bool CanUseAbility() => Time.time >= _lastShotTime + PlayerFireArrowShotData.bowShotCooldown && !IsTouchingCeiling && IsGrounded;
}
