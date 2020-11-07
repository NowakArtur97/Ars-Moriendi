using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    protected bool IsAiming;
    protected bool IsShooting;

    protected D_PlayerBowArrowShotData PlayerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        PlayerFireArrowShotData = playerFireArrowShotData;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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
