using UnityEngine;

public abstract class RangedAttackState : AttackState
{
    protected D_RangedAttackState StateData;

    protected GameObject Projectile;
    protected Projectile ProjectileScript;

    private AttackDetails _attackDetails;

    public RangedAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition, D_RangedAttackState stateData)
        : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Projectile = GameObject.Instantiate(StateData.projectile, AttackPosition.position, AttackPosition.rotation);
        ProjectileScript = Projectile.GetComponent<Projectile>();

        _attackDetails.damageAmmount = StateData.projectileDamage;
        _attackDetails.stunDamageAmount = StateData.projectileStunDamage;

        ProjectileScript.FireProjectile(StateData.projectileSpeed, StateData.projectileTravelDistance, _attackDetails, StateData.projectileGravityScale);
    }
}
