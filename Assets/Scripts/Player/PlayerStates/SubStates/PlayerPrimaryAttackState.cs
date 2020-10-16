using System;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAttackState
{
    private AttackDetails _attackDetails;

    private float _lastArrackTime = Mathf.NegativeInfinity;

    public PlayerPrimaryAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        IsAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();

        _attackDetails.position = attackPosition.position;
        _attackDetails.damageAmmount = PlayerData.attackDamage;
        _attackDetails.stunDamageAmount = PlayerData.stunDamageAmount;

        Player.SetVelocityX(PlayerData.attackMovementSpeed);

        _lastArrackTime = Time.time;
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, PlayerData.attackRadius, PlayerData.whatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", _attackDetails);
        }
    }

    public bool CanAttack()
    {
        Player.InputHandler.UsePrimaryAttackInput();

        return IsAbilityDone && Time.time >= _lastArrackTime + PlayerData.attackCooldown;
    }
}
