using UnityEngine;

public class PlayerSwordAttackState_02 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_02(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, int comboAttackIndex)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, comboAttackIndex)
    {
    }
}
