using UnityEngine;

public abstract class RangedAttackState : AttackState
{
    protected D_RangedAttackState StateData;

    protected GameObject Projectile;
    protected Projectile ProjectileScript;

    public RangedAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition, D_RangedAttackState stateData)
        : base(finiteStateMachine, entity, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Projectile = GameObject.Instantiate(StateData.projectile, AttackPosition.position, AttackPosition.rotation);
        ProjectileScript = Projectile.GetComponent<Projectile>();
        ProjectileScript.FireProjectile(StateData.projectileSpeed, StateData.projectileTravelDistance, StateData.projectileDamage,
            StateData.projectileGravityScale);
    }
}
