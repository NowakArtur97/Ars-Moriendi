using UnityEngine;

public abstract class PlayerSwordAttackState : PlayerAttackState
{
    private D_PlayerSwordAttackState _swordAttackStateData;

    private const string SWORD_ATTACK_ANIMATION_BOOL_NAME = "swordAttack0";

    private int _attackCount;
    private AttackDetails _attackDetails;
    private int _xInput;
    private bool _isAttacking;
    private bool _isAttemptingToAttack;

    public PlayerSwordAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition, D_PlayerSwordAttackState swordAttackStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, attackPosition)
    {
        _swordAttackStateData = swordAttackStateData;
    }

    public override void Enter()
    {
        SetAnimationBoolName(SWORD_ATTACK_ANIMATION_BOOL_NAME + _swordAttackStateData.comboAttackIndex);

        base.Enter();

        Player.InputHandler.UsePrimaryAttackInput();

        _attackDetails.position = attackPosition.position;
        _attackDetails.damageAmmount = _swordAttackStateData.attackDamage;
        _attackDetails.stunDamageAmount = _swordAttackStateData.stunDamageAmount;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;
        _attackCount = Player.InputHandler.PrimaryAttackClickCount;
        _isAttemptingToAttack = Player.InputHandler.PrimaryAttackInput;

        Player.CheckIfShouldFlip(_xInput);
        Player.SetVelocityX(_swordAttackStateData.attackVelocity * _xInput);

        if (IsExitingState && _isAttemptingToAttack)
        {
            if (_attackCount == Player.SwordAttackState01._swordAttackStateData.comboAttackIndex)
            {
                FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState01);
            }
            else if (_attackCount == Player.SwordAttackState02._swordAttackStateData.comboAttackIndex)
            {
                FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState02);
            }
            else if (_attackCount == Player.SwordAttackState03._swordAttackStateData.comboAttackIndex)
            {
                FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState03);
            }
        }
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

    public override void AnimationFinishedTrigger()
    {
        Player.MyAnmator.SetBool(AnimationBoolName, false);

        base.AnimationFinishedTrigger();
    }
}
