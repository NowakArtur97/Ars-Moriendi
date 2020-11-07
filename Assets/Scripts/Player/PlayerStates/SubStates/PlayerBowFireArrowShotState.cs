using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    protected bool IsAiming;

    private D_PlayerBowArrowShotData _playerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        IsAiming = false;
        _playerFireArrowShotData = playerFireArrowShotData;
    }
}
