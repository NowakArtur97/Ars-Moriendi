using UnityEngine;

public class PlayerSwordAttackState_02 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_02(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerSwordAttackState swordAttackStateData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, swordAttackStateData)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsExitingState && IsAttemptingToAttack)
        {
            if (IsGrounded)
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState03);
            }
            else
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState01);
            }
        }
    }
}
