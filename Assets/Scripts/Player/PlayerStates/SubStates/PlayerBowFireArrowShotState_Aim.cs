using UnityEngine;

public class PlayerBowFireArrowShotState_Aim : PlayerBowFireArrowShotState
{
    private bool _shotInputStop;
    private Vector2 _shotDirectionInput;
    private Vector2 _shotDirection;
    private float _directionMultiplier;

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

        if (!IsExitingState)
        {
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
        if (IsShootingInTheOppositeDirection())
        {
            _shotDirection.x = PlayerFireArrowShotData.minBowShotAngleX;
        }

        _shotDirection.y = Mathf.Clamp(_shotDirection.y, PlayerFireArrowShotData.minBowShotAngleY, PlayerFireArrowShotData.maxBowShotAngleY);

        GameObject projectile = GameObject.Instantiate(PlayerFireArrowShotData.arrow, attackPosition.position, attackPosition.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(PlayerFireArrowShotData.arrowSpeed, PlayerFireArrowShotData.arrowTravelDistance, PlayerFireArrowShotData.arrowDamage, _shotDirection);
    }

    private bool IsShootingInTheOppositeDirection() =>
                    (_shotDirection.x < 0 && Mathf.FloorToInt(_shotDirection.x) != Player.FacingDirection)
                    || (_shotDirection.x > 0 && Mathf.RoundToInt(_shotDirection.x) != Player.FacingDirection);
}
