using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    protected D_PlayerBowArrowShotState PlayerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerBowArrowShotState playerFireArrowShotData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition)
    {
        PlayerFireArrowShotData = playerFireArrowShotData;
    }
}
