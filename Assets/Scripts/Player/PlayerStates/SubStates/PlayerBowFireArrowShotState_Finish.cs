using UnityEngine;

public class PlayerBowFireArrowShotState_Finish : PlayerBowFireArrowShotState
{
    public PlayerBowFireArrowShotState_Finish(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerFireArrowShotData)
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
