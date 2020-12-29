using UnityEngine;

public class PlayerSwordAttackState_03 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_03(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerSwordAttackState swordAttackStateData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, swordAttackStateData)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsExitingState && IsAttemptingToAttack)
        {
            Player.FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState01);
        }
    }
}
