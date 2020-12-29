using UnityEngine;

public class PlayerSwordAttackState_01 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_01(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
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
                Player.FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState02);
            }
            else
            {
                Player.FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState01);
            }
        }
    }
}
