using UnityEngine;

public abstract class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState StateData;

    protected AttackDetails AttackDetails;

    public MeleeAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData)
        : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        AttackDetails.position = Enemy.AliveGameObject.transform.position;
        AttackDetails.damageAmmount = StateData.attackDamage;
        AttackDetails.stunDamageAmount = StateData.stunDamage;

        Enemy.SetVelocity(StateData.attackMovementSpeed);
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, StateData.attackRadius, StateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", AttackDetails);
        }
    }
}
