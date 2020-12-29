using UnityEngine;

public abstract class PlayerSwordAttackState : PlayerAttackState
{
    private D_PlayerSwordAttackState _swordAttackStateData;

    private AttackDetails _attackDetails;

    protected int XInput;
    protected bool IsGrounded;

    protected bool IsAttemptingToAttack;

    public PlayerSwordAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition, D_PlayerSwordAttackState swordAttackStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, attackPosition)
    {
        _swordAttackStateData = swordAttackStateData;
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
        IsAttemptingToAttack = Player.InputHandler.PrimaryAttackInput;

        Player.CheckIfShouldFlip(XInput);
        Player.SetVelocityX(_swordAttackStateData.attackVelocity * XInput);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position,
            _swordAttackStateData.attackRadius, _swordAttackStateData.whatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", _attackDetails);
        }
    }

    private void SetAttackDetails()
    {
        _attackDetails.position = attackPosition.position;
        _attackDetails.damageAmmount = _swordAttackStateData.attackDamage;
        _attackDetails.stunDamageAmount = _swordAttackStateData.stunDamageAmount;
    }
}
