using UnityEngine;

public class PlayerBowFireArrowShotState_Aim : PlayerBowFireArrowShotState
{
    private bool _shotInputStop;
    private Vector2 _shotDirectionInput;
    private Vector2 _shotDirection;

    public PlayerBowFireArrowShotState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerFireArrowShotData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanShot = false;
        IsAiming = false;
        IsShooting = false;

        Time.timeScale = PlayerFireArrowShotData.holdTimeAimScale;
        StartTime = Time.unscaledTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _shotDirectionInput = Player.InputHandler.RawSecondaryAttackDirectionInput;
        _shotInputStop = Player.InputHandler.SecondaryAttackInputStop;

        if (_shotInputStop || Time.unscaledTime >= PlayerFireArrowShotData.bowShotMaxHoldTime + StartTime)
        {
            Time.timeScale = 1;

            IsShooting = true;

            if (_shotDirectionInput != Vector2.zero)
            {
                _shotDirection = _shotDirectionInput;
                _shotDirection.Normalize();
            }

            Shot();
        }
    }

    public override void Exit()
    {
        base.Exit();

        CanShot = false;
        IsAiming = false;
        IsShooting = true;
    }

    private void Shot()
    {
        GameObject projectile = GameObject.Instantiate(PlayerFireArrowShotData.arrow, attackPosition.position, attackPosition.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(PlayerFireArrowShotData.arrowSpeed, PlayerFireArrowShotData.arrowTravelDistance, PlayerFireArrowShotData.arrowDamage, _shotDirection);
    }
}
