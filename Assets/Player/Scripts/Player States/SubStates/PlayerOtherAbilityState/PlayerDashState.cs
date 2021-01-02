using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private D_PlayerDashState _dashStateData;

    private bool _canDash;
    private float _lastDashTime;

    private bool _isHolding;
    private bool _dashInputStop;
    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector2 _lastAfterImagePosition;

    public PlayerDashState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerDashState dashStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _dashStateData = dashStateData;
    }

    public override void Enter()
    {
        base.Enter();

        _canDash = false;
        Player.InputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * Player.FacingDirection;
        Time.timeScale = _dashStateData.holdTimeDashScale;
        StartTime = Time.unscaledTime;

        Player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (Player.CurrentVelocity.y > 0)
        {
            Player.SetVelocityY(Player.CurrentVelocity.y * _dashStateData.dashEndMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.MyAnmator.SetFloat("xVelocity", Player.CurrentVelocity.x);
            Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);

            if (_isHolding)
            {
                _dashDirectionInput = Player.InputHandler.DashDirectionInput;
                _dashInputStop = Player.InputHandler.DashInputStop;

                if (_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                Player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (_dashInputStop || Time.unscaledTime >= StartTime + _dashStateData.maxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1;
                    StartTime = Time.time;
                    Player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    Player.MyRigidbody.drag = _dashStateData.dashDrag;
                    Player.SetVelocity(_dashStateData.dashVelocity, _dashDirection);
                    Player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                Player.SetVelocity(_dashStateData.dashVelocity, _dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= StartTime + _dashStateData.dashTime)
                {
                    Player.MyRigidbody.drag = 0;
                    IsAbilityDone = true;
                    _lastDashTime = Time.time;
                }
            }
        }
    }

    private void PlaceAfterImage()
    {
        ObjectPoolManager.Instance.GetFromPool(ObjectPoolType.AFTER_IMAGE);
        _lastAfterImagePosition = Player.AliveGameObject.transform.position;
    }

    public void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(Player.AliveGameObject.transform.position, _lastAfterImagePosition) >= _dashStateData.distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    public bool CheckIfCanDash() => _canDash && Time.time >= _lastDashTime + _dashStateData.dashCooldown;

    public bool ResetCanDash() => _canDash = true;
}
