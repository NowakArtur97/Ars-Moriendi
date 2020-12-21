using UnityEngine;

public abstract class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState StateData;

    protected AttackDetails AttackDetails;

    public MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData)
        : base(finiteStateMachine, entity, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        AttackDetails.position = Entity.AliveGameObject.transform.position;
        AttackDetails.damageAmmount = StateData.attackDamage;

        Entity.SetVelocity(StateData.attackMovementSpeed);
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, StateData.attackRadius, StateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", AttackDetails);
        }
    }
}
