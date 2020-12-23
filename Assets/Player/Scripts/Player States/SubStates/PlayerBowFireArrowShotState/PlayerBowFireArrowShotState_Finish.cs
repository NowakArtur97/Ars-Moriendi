using UnityEngine;

public class PlayerBowFireArrowShotState_Finish : PlayerBowFireArrowShotState
{
    public PlayerBowFireArrowShotState_Finish(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerBowArrowShotState playerFireArrowShotData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanShot = true;
        IsAiming = false;
        IsShooting = false;
    }

    public override void Exit()
    {
        base.Exit();

        LastShotTime = Time.time;

        CanShot = true;
        IsAiming = false;
        IsShooting = false;
    }
}
