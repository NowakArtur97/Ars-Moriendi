using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAttackState
{
    private AttackDetails _attackDetails;
    private int _xInput;

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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;

        Player.CheckIfShouldFlip(_xInput);
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
