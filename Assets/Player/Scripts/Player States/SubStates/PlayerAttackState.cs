using UnityEngine;

public abstract class PlayerAttackState : PlayerAbilityState
{
    protected Transform attackPosition;

    public PlayerAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition) : base(player, playerFiniteStateMachine, animationBoolName)
    {
        this.attackPosition = attackPosition;
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
