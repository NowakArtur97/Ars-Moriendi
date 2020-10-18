using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private float _lastAttackTime;

    protected Transform attackPosition;

    public PlayerAttackState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName,
        Transform attackPosition) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        _lastAttackTime = Time.time;
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

        Player.MyAnmator.SetBool(AnimationBoolName, false);

        IsAbilityDone = true;
    }

    public bool CanAttack() => IsAbilityDone && Time.time >= _lastAttackTime + PlayerData.attackCooldown;
}
