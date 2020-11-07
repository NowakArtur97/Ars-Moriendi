using UnityEngine;

public class PlayerBowFireArrowShotState_Aim : PlayerBowFireArrowShotState
{
    private bool _shotInputStop;

    public PlayerBowFireArrowShotState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = PlayerFireArrowShotData.holdTimeAimScale;
        StartTime = Time.unscaledTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _shotInputStop = Player.InputHandler.SecondaryAttackInputStop;

        if (_shotInputStop || Time.unscaledTime >= PlayerFireArrowShotData.bowShotMaxHoldTime + StartTime)
        {
            IsAiming = false;
            IsShooting = true;
            Time.timeScale = 1;

            Shot();
        }
    }

    private void Shot()
    {
        GameObject projectile = GameObject.Instantiate(PlayerFireArrowShotData.arrow, attackPosition.position, attackPosition.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(PlayerFireArrowShotData.arrowSpeed, PlayerFireArrowShotData.arrowTravelDistance, PlayerFireArrowShotData.arrowDamage);
    }
}
