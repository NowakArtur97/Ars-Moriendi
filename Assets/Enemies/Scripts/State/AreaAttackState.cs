using UnityEngine;

public class AreaAttackState : AttackState
{
    protected D_AreaAttackState StateData;

    protected GameObject Projectile;
    protected Projectile ProjectileScript;

    private AttackDetails _attackDetails;

    private float _xPosition;
    private float _yPosition;
    private Vector2 _projectilePosition;
    private Vector2 _projectileDirection;
    private float _projectileAngle;

    public AreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition, D_AreaAttackState stateData)
        : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        int numberOfProjectiles = StateData.numberOfProjectiles;
        _projectileAngle = StateData.initialAngle;
        float incrementAngle = (StateData.areaAngle - (2 * _projectileAngle)) / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            CreateProjectile(numberOfProjectiles, incrementAngle);

            SetupAttackDetails();

            ProjectileScript.FireProjectile(StateData.projectileSpeed, StateData.projectileTravelDistance, _attackDetails, StateData.projectileGravityScale,
                _projectileDirection);
        }
    }

    private void CreateProjectile(int numberOfProjectilse, float incrementAngle)
    {
        // 2 beacause we using radians
        //_projectileAngle = projectileIndex * 2 * Mathf.PI / numberOfProjectilse;

        //_xPosition = Mathf.Cos(_projectileAngle ) * StateData.areaRadius ;
        //_yPosition = Mathf.Sin(_projectileAngle) * StateData.areaRadius;
        _xPosition = Mathf.Cos(_projectileAngle * Mathf.PI / 180) * StateData.areaRadius;
        _yPosition = Mathf.Sin(_projectileAngle * Mathf.PI / 180) * StateData.areaRadius;

        _projectilePosition.Set(AttackPosition.transform.position.x + _xPosition, AttackPosition.position.y + _yPosition);

        Projectile = GameObject.Instantiate(StateData.projectile, _projectilePosition, AttackPosition.rotation);
        ProjectileScript = Projectile.GetComponent<Projectile>();

        _projectileDirection = (_projectilePosition - (Vector2)AttackPosition.transform.position).normalized;

        Projectile.transform.parent = AttackPosition;
        _projectileAngle += incrementAngle;
    }

    private void SetupAttackDetails()
    {
        _attackDetails.damageAmmount = StateData.projectileDamage;
        _attackDetails.stunDamageAmount = StateData.projectileStunDamage;
    }
}
