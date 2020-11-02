using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    protected GameObject projectile;
    protected Projectile projectileScript;

    private D_PlayerBowArrowShotData _playerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        _playerFireArrowShotData = playerFireArrowShotData;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = GameObject.Instantiate(_playerFireArrowShotData.arrow, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(_playerFireArrowShotData.arrowSpeed, _playerFireArrowShotData.arrowTravelDistance, _playerFireArrowShotData.arrowDamage);
    }
}
