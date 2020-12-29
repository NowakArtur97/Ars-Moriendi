using UnityEngine;

public abstract class PlayerAttackState : PlayerAbilityState
{
    protected Transform AttackPosition;

    public PlayerAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        AttackPosition = attackPosition;
    }

    public override void LogicUpdate()
    {
        if (IsAnimationFinished)
        {
            IsAbilityDone = true;
        }

        base.LogicUpdate();
    }
}
