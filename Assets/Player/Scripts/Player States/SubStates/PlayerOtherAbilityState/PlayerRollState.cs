using UnityEngine;

public class PlayerRollState : PlayerAbilityState
{
    private D_PlayerRollState _rollStateData;

    private bool _isTouchingCeiling;

    public PlayerRollState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerRollState rollStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _rollStateData = rollStateData;
    }

    public override void Enter()
    {
        Player.SetBoxColliderHeight(_rollStateData.rollColliderHeight);

        base.Enter();

        Player.SetVelocityX(Player.FacingDirection * _rollStateData.rollVelocity);

        Player.StatsManager.SetIsRolling(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            IsAbilityDone = true;
        }

        if (!IsExitingState)
        {
            if (_isTouchingCeiling)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchIdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        Player.SetVelocityZero();

        Player.StatsManager.SetIsRolling(false);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //_isTouchingCeiling = Player.CheckIfTouchingCeiling();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isTouchingCeiling = Player.CheckIfTouchingCeiling();

        Player.MyAnmator.SetBool("isTouchingCeiling", _isTouchingCeiling);
    }
}
