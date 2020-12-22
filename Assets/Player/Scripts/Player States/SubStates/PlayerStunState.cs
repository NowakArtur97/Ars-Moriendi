﻿using UnityEngine;

public class PlayerStunState : PlayerState
{
    private D_PlayerStunStateData _stunStateData;

    private bool _isGrounded;

    public PlayerStunState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerStunStateData stunStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _stunStateData = stunStateData;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= Player.StatsManager.LastDamageTime + _stunStateData.stunRecorveryTime)
        {
            Player.StatsManager.ResetStunResistance();

            if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                FiniteStateMachine.ChangeCurrentState(Player.IdleState);
            }
            else
            {
                FiniteStateMachine.ChangeCurrentState(Player.InAirState);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
    }

}
