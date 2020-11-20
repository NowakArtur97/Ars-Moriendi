using UnityEngine;

public class PlayerBowFireArrowShotState_Start : PlayerBowFireArrowShotState
{
    public PlayerBowFireArrowShotState_Start(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerFireArrowShotData)
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
