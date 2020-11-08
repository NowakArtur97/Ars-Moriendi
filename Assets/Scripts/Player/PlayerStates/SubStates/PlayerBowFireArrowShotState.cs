using UnityEngine;

public class PlayerBowFireArrowShotState : PlayerAttackState
{
    private bool _isAiming;
    private bool _isShooting;
    private bool _canShot;
    private float _lastShotTime;

    protected D_PlayerBowArrowShotData PlayerFireArrowShotData;

    public PlayerBowFireArrowShotState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData,
        string animationBoolName, Transform attackPosition, D_PlayerBowArrowShotData playerFireArrowShotData)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName, attackPosition)
    {
        PlayerFireArrowShotData = playerFireArrowShotData;
        _canShot = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (_isAiming)
            {
                Player.FiniteStateMachine.ChangeState(Player.FireArrowShotStateAim);
            }
            else if (_isShooting)
            {
                Player.FiniteStateMachine.ChangeState(Player.FireArrowShotStateFinish);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        _isShooting = false;
        _isAiming = false;
        _canShot = true;
    }

    public void IsAiming(bool isAiming) => _isAiming = isAiming;

    public void IsShooting(bool isShooting) => _isShooting = isShooting;

    public void SetCanShot(bool canShot) => _canShot = canShot;

    public void SetLastShotTime(float lastShotTime) => _lastShotTime = lastShotTime;

    public bool CheckIfCanShoot() => _canShot && Time.time >= _lastShotTime + PlayerFireArrowShotData.bowShotCooldown;
}
