using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    private bool _canShoot;
    private float _lastShotTime;
    private bool _isHolding;
    private bool _shotInputStop;
    private Vector2 _shotDirection;
    private Vector2 _shotDirectionInput;

    protected GameObject projectile;
    protected Projectile projectileScript;

    private D_PlayerBowArrowShotData _playerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData) : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        _playerFireArrowShotData = playerFireArrowShotData;
        _canShoot = true;
    }

    public override void Enter()
    {
        base.Enter();

        _canShoot = false;
        _isHolding = true;
        _shotDirection = Vector2.right * Player.FacingDirection;
        Time.timeScale = PlayerData.holdTimeDashScale;
        StartTime = Time.unscaledTime;
    }

    public override void Exit()
    {
        base.Exit();

        _canShoot = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (_isHolding)
            {
                _shotDirectionInput = Player.InputHandler.SecondaryAttackDirectionInput;
                _shotInputStop = Player.InputHandler.SecondaryAttackInputStop;

                if (_shotDirectionInput != Vector2.zero)
                {
                    _shotDirection = _shotDirectionInput;
                    _shotDirection.Normalize();
                }

                if (_shotInputStop || Time.unscaledTime >= StartTime + _playerFireArrowShotData.bowShotMaxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1;
                    StartTime = Time.time;
                    Player.CheckIfShouldFlip(Mathf.RoundToInt(_shotDirection.x));
                }
            }
            else
            {
                GameObject arrow = GameObject.Instantiate(_playerFireArrowShotData.arrow, attackPosition.position, attackPosition.rotation);
                projectileScript = arrow.GetComponent<Projectile>();
                projectileScript.FireProjectile(_playerFireArrowShotData.arrowSpeed, _playerFireArrowShotData.arrowTravelDistance, _playerFireArrowShotData.arrowDamage);
                IsAbilityDone = true;
                _lastShotTime = Time.time;
            }
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = GameObject.Instantiate(_playerFireArrowShotData.arrow, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(_playerFireArrowShotData.arrowSpeed, _playerFireArrowShotData.arrowTravelDistance, _playerFireArrowShotData.arrowDamage);
    }

    public bool CheckIfCanShoot() => _canShoot && Time.time >= _lastShotTime + _playerFireArrowShotData.bowShotCooldown;
}
