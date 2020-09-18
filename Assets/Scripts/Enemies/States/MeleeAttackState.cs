using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;

    protected AttackDetails attackDetails;

    public MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData)
        : base(finiteStateMachine, entity, animationBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.position = entity.aliveGameObject.transform.position;
        attackDetails.damageAmmount = stateData.attackDamage;

        entity.SetVelocity(stateData.attackMovementSpeed);

        DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();

        DoChecks();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}
