using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private float _lastDashTime;

    public PlayerDashState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public bool CheckIfCanDash() => CanDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;

    public bool ResetCanDash() => CanDash = true;
}
