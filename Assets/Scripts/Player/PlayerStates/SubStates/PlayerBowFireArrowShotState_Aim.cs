using UnityEngine;

public class PlayerBowFireArrowShotState_Aim : PlayerBowFireArrowShotState
{
    public PlayerBowFireArrowShotState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }
}
