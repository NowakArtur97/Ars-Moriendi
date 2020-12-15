using UnityEngine;

public class PlayerSwordAttackState : PlayerAttackState
{
    private D_PlayerSwordAttackData _playerSwordAttackData;

    private const string SWORD_ATTACK_ANIMATION_BOOL_NAME = "swordAttack0";

    private int _attackCount;
    private AttackDetails _attackDetails;
    private int _xInput;
    private bool _isAttacking;
    private bool _isAttemptingToAttack;

    public PlayerSwordAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName,
        Transform attackPosition, D_PlayerSwordAttackData playerSwordAttackData)
        : base(player, playerFiniteStateMachine, animationBoolName, attackPosition)
    {
        _playerSwordAttackData = playerSwordAttackData;
    }

    public override void Enter()
    {
        SetAnimationBoolName(SWORD_ATTACK_ANIMATION_BOOL_NAME + _playerSwordAttackData.comboAttackIndex);

        base.Enter();

        Player.InputHandler.UsePrimaryAttackInput();

        _attackDetails.position = attackPosition.position;
        _attackDetails.damageAmmount = _playerSwordAttackData.attackDamage;
        _attackDetails.stunDamageAmount = _playerSwordAttackData.stunDamageAmount;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;
        _attackCount = Player.InputHandler.PrimaryAttackClickCount;
        _isAttemptingToAttack = Player.InputHandler.PrimaryAttackInput;

        Player.CheckIfShouldFlip(_xInput);
        Player.SetVelocityX(_playerSwordAttackData.attackVelocity * _xInput);

        if (IsExitingState && _isAttemptingToAttack)
        {
            if (_attackCount == Player.SwordAttackState01._playerSwordAttackData.comboAttackIndex)
            {
                FiniteStateMachine.ChangeState(Player.SwordAttackState01);
            }
            else if (_attackCount == Player.SwordAttackState02._playerSwordAttackData.comboAttackIndex)
            {
                FiniteStateMachine.ChangeState(Player.SwordAttackState02);
            }
            else if (_attackCount == Player.SwordAttackState03._playerSwordAttackData.comboAttackIndex)
            {
                FiniteStateMachine.ChangeState(Player.SwordAttackState03);
            }
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position,
            _playerSwordAttackData.attackRadius, _playerSwordAttackData.whatIsEnemy);

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
