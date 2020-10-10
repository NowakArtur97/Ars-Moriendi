using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private float _lastDashTime;

    private bool _isHolding;
    private Vector2 _dashDirection;

    public PlayerDashState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        Player.InputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * Player.FacingDirection;
        Time.timeScale = PlayerData.holdTimeDashScale;
        StartTime = Time.unscaledTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (_isHolding)
            {

            }
        }
    }

    public bool CheckIfCanDash() => CanDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;

    public bool ResetCanDash() => CanDash = true;
}
