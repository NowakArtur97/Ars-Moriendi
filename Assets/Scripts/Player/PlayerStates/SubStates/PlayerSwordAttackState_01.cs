using UnityEngine;

public class PlayerSwordAttackState_01 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_01(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName, Transform attackPosition, D_PlayerSwordAttackData playerSwordAttackData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerSwordAttackData)
    {
    }
}
