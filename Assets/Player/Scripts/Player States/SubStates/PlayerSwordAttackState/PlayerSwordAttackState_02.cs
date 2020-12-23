using UnityEngine;

public class PlayerSwordAttackState_02 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_02(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerSwordAttackState swordAttackStateData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, swordAttackStateData)
    {
    }
}
