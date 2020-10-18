using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAttackState
{
    private AttackDetails _attackDetails;

    public PlayerPrimaryAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _attackDetails.position = attackPosition.position;
        _attackDetails.damageAmmount = PlayerData.attackDamage;
        _attackDetails.stunDamageAmount = PlayerData.stunDamageAmount;
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
}
