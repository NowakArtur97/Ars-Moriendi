using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowFireArrowShotData _playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
    }
}
