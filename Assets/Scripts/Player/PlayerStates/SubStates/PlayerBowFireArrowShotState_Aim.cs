using UnityEngine;

public class PlayerBowFireArrowShotState_Aim : PlayerBowFireArrowShotState
{
    private bool _shotInputStop;
    private Vector2 _shotDirectionInput;
    private Vector2 _shotDirection;
    private Vector2 _aimDirection;
    private float _directionMultiplier;

    private GameObject[] _points;

    public PlayerBowFireArrowShotState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition, playerFireArrowShotData)
    {
        InitializeAimingPointsArray(attackPosition, playerFireArrowShotData);
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
            _aimDirection = _shotDirectionInput.normalized;

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
            else
            {
                Aim();
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

    private void Aim()
    {
        int numberOfAimingPoints = PlayerFireArrowShotData.numberOfAimingPoints;
        float spaceBetweenAimingPoints = PlayerFireArrowShotData.spaceBetweenAimingPoints;

        for (int i = 0; i < numberOfAimingPoints; i++)
        {
            _points[i].transform.position = PointToPosition(i * spaceBetweenAimingPoints);
        }
    }

    private Vector2 PointToPosition(float time) => (Vector2)attackPosition.position + (_aimDirection * PlayerFireArrowShotData.arrowSpeed * time)
      + 0.5f * Physics2D.gravity * (time * time);


    private bool IsShootingInTheOppositeDirection() =>
                    (_shotDirection.x < 0 && Mathf.FloorToInt(_shotDirection.x) != Player.FacingDirection)
                    || (_shotDirection.x > 0 && Mathf.RoundToInt(_shotDirection.x) != Player.FacingDirection);

    private void InitializeAimingPointsArray(Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData)
    {
        _points = new GameObject[playerFireArrowShotData.numberOfAimingPoints];

        GameObject aimingPoint = playerFireArrowShotData.aimingPoint;
        int numberOfAimingPoints = playerFireArrowShotData.numberOfAimingPoints;

        for (int i = 0; i < numberOfAimingPoints; i++)
        {
            _points[i] = GameObject.Instantiate(aimingPoint, attackPosition.position, Quaternion.identity);
        }
    }
}
