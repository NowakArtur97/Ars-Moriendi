﻿using UnityEngine;

public class PlayerBowFireArrowShotState_Aim : PlayerBowFireArrowShotState
{
    private bool _shotInputStop;
    private Vector2 _shotDirectionInput;
    private Vector2 _shotDirection;
    private float _directionMultiplier;

    private int _numberOfAimingPoints;
    private float _spaceBetweenAimingPoints;
    private float _arrowSpeed;

    private GameObject[] _points;

    private AttackDetails _attackDetails;

    public PlayerBowFireArrowShotState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerBowArrowShotState playerFireArrowShotData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, playerFireArrowShotData)
    {
        InitializeAimingPointsArray(attackPosition, playerFireArrowShotData);
    }

    public override void Enter()
    {
        base.Enter();

        _numberOfAimingPoints = PlayerFireArrowShotData.numberOfAimingPoints;
        _spaceBetweenAimingPoints = PlayerFireArrowShotData.spaceBetweenAimingPoints;
        _arrowSpeed = PlayerFireArrowShotData.arrowSpeed;

        Time.timeScale = PlayerFireArrowShotData.holdTimeAimScale;
        StartTime = Time.unscaledTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            _shotDirectionInput = Player.InputHandler.RawSecondaryAttackDirectionInput;
            _shotInputStop = Player.InputHandler.SecondaryInputStop;
            _shotDirection = _shotDirectionInput;

            if (_shotInputStop || Time.unscaledTime >= PlayerFireArrowShotData.bowShotMaxHoldTime + StartTime)
            {
                Time.timeScale = 1;

                if (_shotDirectionInput != Vector2.zero)
                {
                    _shotDirection.Normalize();
                }

                Shot();

                Player.FiniteStateMachine.ChangeCurrentState(Player.FireArrowShotStateFinish);
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

        Time.timeScale = 1;
        HideAimingPoints();
    }

    private void Shot()
    {
        ClampShotDirection();

        GameObject projectile = GameObject.Instantiate(PlayerFireArrowShotData.arrow, AttackPosition.position, AttackPosition.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        _attackDetails.damageAmmount = PlayerFireArrowShotData.arrowDamage;
        _attackDetails.stunDamageAmount = PlayerFireArrowShotData.arrowStunDamage;

        projectileScript.FireProjectile(_arrowSpeed, PlayerFireArrowShotData.arrowTravelDistance, _attackDetails,
            PlayerFireArrowShotData.arrowGravityScale, _shotDirection);
    }

    private void Aim()
    {
        _shotDirection.Normalize();

        ClampShotDirection();

        for (int i = 0; i < _numberOfAimingPoints; i++)
        {
            _points[i].transform.position = PointToPosition(i * _spaceBetweenAimingPoints, _arrowSpeed);
            _points[i].SetActive(true);
        }
    }

    private Vector2 PointToPosition(float time, float arrowSpeed) => (Vector2)AttackPosition.position + (_shotDirection * arrowSpeed * time)
      + 0.5f * Physics2D.gravity * (time * time);

    private bool IsShootingInTheOppositeDirection() =>
                    (_shotDirection.x < 0 && Mathf.FloorToInt(_shotDirection.x) != Player.FacingDirection)
                    || (_shotDirection.x > 0 && Mathf.RoundToInt(_shotDirection.x) != Player.FacingDirection);

    private void ClampShotDirection()
    {
        if (IsShootingInTheOppositeDirection())
        {
            _shotDirection.x = Player.FacingDirection * PlayerFireArrowShotData.minBowShotAngleX;
        }

        _shotDirection.y = Mathf.Clamp(_shotDirection.y, PlayerFireArrowShotData.minBowShotAngleY, PlayerFireArrowShotData.maxBowShotAngleY);
    }

    private void InitializeAimingPointsArray(Transform attackPosition, D_PlayerBowArrowShotState playerFireArrowShotData)
    {
        _points = new GameObject[playerFireArrowShotData.numberOfAimingPoints];

        GameObject aimingPoint = playerFireArrowShotData.aimingPoint;
        int numberOfAimingPoints = playerFireArrowShotData.numberOfAimingPoints;

        for (int i = 0; i < numberOfAimingPoints; i++)
        {
            _points[i] = GameObject.Instantiate(aimingPoint, attackPosition.position, Quaternion.identity);
            _points[i].transform.SetParent(Player.AimingPointWrapper);
            _points[i].SetActive(false);
        }
    }

    private void HideAimingPoints()
    {
        for (int i = 0; i < _numberOfAimingPoints; i++)
        {
            _points[i].SetActive(false);
        }
    }
}
