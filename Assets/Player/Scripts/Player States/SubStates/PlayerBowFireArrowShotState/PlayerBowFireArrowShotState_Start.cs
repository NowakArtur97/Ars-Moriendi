using UnityEngine;

public class PlayerBowFireArrowShotState_Start : PlayerBowFireArrowShotState
{
    public PlayerBowFireArrowShotState_Start(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition, D_PlayerBowArrowShotState playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityX(0.0f);

        CanShot = false;
        IsAiming = false;
        IsShooting = false;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        IsAiming = true;
    }
}
