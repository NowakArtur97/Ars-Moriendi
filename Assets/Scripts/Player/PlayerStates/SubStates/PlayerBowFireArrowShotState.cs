using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    protected bool IsAiming;
    protected bool IsShooting;
    protected bool CanShot;
    protected float LastShotTime;

    protected D_PlayerBowArrowShotData PlayerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        PlayerFireArrowShotData = playerFireArrowShotData;
        CanShot = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (IsAiming)
            {
                Player.FiniteStateMachine.ChangeState(Player.FireArrowShotStateAim);
            }
            else if (IsShooting)
            {
                Player.FiniteStateMachine.ChangeState(Player.FireArrowShotStateFinish);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        IsShooting = false;
        IsAiming = false;
        CanShot = true;
    }

    public bool CheckIfCanShoot() => CanShot && Time.time >= LastShotTime + PlayerFireArrowShotData.bowShotCooldown;
}
