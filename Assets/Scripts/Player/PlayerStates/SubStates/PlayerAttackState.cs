using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Transform attackPosition;

    public PlayerAttackState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName,
        Transform attackPosition) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (IsAnimationFinished)
            {
                IsAbilityDone = true;
            }
        }
    }
}
