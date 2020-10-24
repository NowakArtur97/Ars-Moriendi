using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Transform attackPosition;

    public PlayerAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName,
        Transform attackPosition) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            IsAbilityDone = true;
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAbilityDone = true;
    }
}
