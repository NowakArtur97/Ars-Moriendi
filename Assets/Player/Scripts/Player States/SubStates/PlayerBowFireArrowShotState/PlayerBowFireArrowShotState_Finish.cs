using UnityEngine;

public class PlayerBowFireArrowShotState_Finish : PlayerBowFireArrowShotState
{
    public PlayerBowFireArrowShotState_Finish(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerBowArrowShotState playerFireArrowShotData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }
}
