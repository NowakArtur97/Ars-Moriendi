using UnityEngine;

public abstract class PlayerSwordAttackState : PlayerAttackState
{
    private AttackDetails _attackDetails;

    protected D_PlayerSwordAttackState SwordAttackStateData;

    protected int XInput;
    protected bool IsGrounded;

    protected bool IsAttemptingToAttack;

    public PlayerSwordAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition, D_PlayerSwordAttackState swordAttackStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, attackPosition)
    {
        SwordAttackStateData = swordAttackStateData;
    }

    public override void Enter()
    {
        base.Enter();

        Player.InputHandler.UsePrimaryAttackInput();

        SetAttackDetails();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        IsAttemptingToAttack = Player.InputHandler.PrimaryInput;

        Player.CheckIfShouldFlip(XInput);
        Player.SetVelocityX(SwordAttackStateData.attackVelocity * XInput);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position,
            SwordAttackStateData.attackRadius, SwordAttackStateData.whatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", _attackDetails);
        }
    }

    private void SetAttackDetails()
    {
        _attackDetails.position = AttackPosition.position;
        _attackDetails.damageAmmount = SwordAttackStateData.attackDamage;
        _attackDetails.stunDamageAmount = SwordAttackStateData.stunDamageAmount;
    }
}
