﻿using UnityEngine;

public class AreaAttackState : AttackState
{
    protected D_AreaAttackState StateData;

    protected GameObject Projectile;
    protected Projectile ProjectileScript;

    private AttackDetails _attackDetails;

    private float _xPosition;
    private float _yPosition;
    private Vector2 _projectilePosition;
    private float _projectileAngle;

    public AreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition, D_AreaAttackState stateData)
        : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        int numberOfProjectilse = StateData.numberOfProjectiles;

        for (int i = 0; i < numberOfProjectilse; i++)
        {
            _projectileAngle = i * (2 * Mathf.PI / numberOfProjectilse);

            _xPosition = Mathf.Cos(_projectileAngle) * 1.5f;
            _yPosition = Mathf.Sin(_projectileAngle) * 1.5f;

            _projectilePosition.Set(Enemy.AliveGameObject.transform.position.x + _xPosition, Enemy.AliveGameObject.transform.position.y + _yPosition);

            Projectile = GameObject.Instantiate(StateData.projectile, _projectilePosition, AttackPosition.rotation);
            ProjectileScript = Projectile.GetComponent<Projectile>();

            _attackDetails.damageAmmount = StateData.projectileDamage;
            _attackDetails.stunDamageAmount = StateData.projectileStunDamage;

            ProjectileScript.FireProjectile(StateData.projectileSpeed, StateData.projectileTravelDistance, _attackDetails, StateData.projectileGravityScale);
        }
    }
}
