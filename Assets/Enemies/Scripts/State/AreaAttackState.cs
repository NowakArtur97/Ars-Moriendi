using UnityEngine;

public class AreaAttackState : AttackState
{
    protected D_AreaAttackState StateData;

    private GameObject _projectilePrefab;
    protected GameObject Projectile;
    protected Projectile ProjectileScript;

    private AttackDetails _attackDetails;

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
        float projectileSpeed = StateData.projectileSpeed;
        float projectileTravelDistance = StateData.projectileTravelDistance;
        float projectileGravityScale = StateData.projectileGravityScale;
        _projectileAngle = StateData.initialAngle;
        _projectilePrefab = StateData.projectile;

        for (int projectileIndex = 0; projectileIndex < numberOfProjectiles; projectileIndex++)
        {
            CreateProjectile(numberOfProjectiles);

            SetupAttackDetails();

            ProjectileScript.FireProjectile(projectileSpeed, projectileTravelDistance, _attackDetails, projectileGravityScale, _projectileDirection);
        }
    }

    private void CreateProjectile(int numberOfProjectilse)
    {
        float xPosition = Mathf.Cos(_projectileAngle * Mathf.PI / StateData.areaAngle) * StateData.areaRadius;
        float yPosition = Mathf.Sin(_projectileAngle * Mathf.PI / StateData.areaAngle) * StateData.areaRadius;

        _projectilePosition.Set(AttackPosition.transform.position.x + xPosition, AttackPosition.position.y + yPosition);

        Projectile = GameObject.Instantiate(_projectilePrefab, _projectilePosition, Quaternion.identity);
        ProjectileScript = Projectile.GetComponent<Projectile>();

        _projectileDirection = (_projectilePosition - (Vector2)AttackPosition.transform.position).normalized;

        _projectileAngle += StateData.incrementAngle;
    }

    private void SetupAttackDetails()
    {
        _attackDetails.damageAmmount = StateData.projectileDamage;
        _attackDetails.stunDamageAmount = StateData.projectileStunDamage;
    }
}
