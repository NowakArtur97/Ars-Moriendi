using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAttackState
{
    private AttackDetails attackDetails;

    public PlayerPrimaryAttackState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName,
        Transform attackPosition) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName, attackPosition)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.InputHandler.UsePrimaryAttackInput();

        attackDetails.position = attackPosition.position;
        attackDetails.damageAmmount = PlayerData.attackDamage;
        attackDetails.stunDamageAmount = PlayerData.stunDamageAmount;

        Player.SetVelocityX(PlayerData.attackMovementSpeed);
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, PlayerData.attackRadius, PlayerData.whatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }
}
