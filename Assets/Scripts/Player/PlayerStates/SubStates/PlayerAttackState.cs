using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Transform attackPosition;

    public PlayerAttackState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName,
        Transform attackPosition) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
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

        Player.MyAnmator.SetBool("primaryAttack", false);
    }
}
