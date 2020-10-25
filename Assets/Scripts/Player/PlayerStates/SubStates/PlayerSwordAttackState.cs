using UnityEngine;

public class PlayerSwordAttackState : PlayerAttackState
{
    private const string SWORD_ATTACK_ANIMATION_BOOL_NAME = "swordAttack0";
    private int _comboAttackIndex;

    private int _attackCount;
    private AttackDetails _attackDetails;
    private int _xInput;
    private bool _isAttacking;
    private bool _isAttemptingToAttack;

    public PlayerSwordAttackState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, int comboAttackIndex) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        _comboAttackIndex = comboAttackIndex;
    }

    public override void Enter()
    {
        SetAnimationBoolName(SWORD_ATTACK_ANIMATION_BOOL_NAME + _comboAttackIndex);

        base.Enter();

        Player.InputHandler.UsePrimaryAttackInput();

        _attackDetails.position = attackPosition.position;
        _attackDetails.damageAmmount = PlayerData.attackDamage;
        _attackDetails.stunDamageAmount = PlayerData.stunDamageAmount;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;
        _attackCount = Player.InputHandler.PrimaryAttackClickCount;
        _isAttemptingToAttack = Player.InputHandler.PrimaryAttackInput;

        Player.CheckIfShouldFlip(_xInput);
        Player.SetVelocityX(PlayerData.attackVelocity * _xInput);

        if (IsExitingState && _isAttemptingToAttack)
        {
            if (_attackCount == Player.SwordAttackState01._comboAttackIndex)
            {
                FiniteStateMachine.ChangeState(Player.SwordAttackState01);
            }
            else if (_attackCount == Player.SwordAttackState02._comboAttackIndex)
            {
                FiniteStateMachine.ChangeState(Player.SwordAttackState02);
            }
            else if (_attackCount == Player.SwordAttackState03._comboAttackIndex)
            {
                FiniteStateMachine.ChangeState(Player.SwordAttackState03);
            }
        }
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

    public override void AnimationFinishedTrigger()
    {
        Player.MyAnmator.SetBool(AnimationBoolName, false);

        base.AnimationFinishedTrigger();
    }
}
