﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState_Aim : PlayerOnRopeState
{
    private Vector2 _ropeDirectionInput;
    private Vector3 _crossHairPosition;
    private float _aimAngle;

    public PlayerOnRopeState_Aim(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("PlayerOnRopeState_Aim");

        RopeAttached = false;
        IsAiming = false;
        IsHoldingRope = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (RopeInputStop)
            {
                RopeAttached = false;
                IsAiming = false;

                Player.InputHandler.UseSecondaryAttackInputStop();
            }

            PlayerPosition = Player.transform.position;
            _ropeDirectionInput = Player.InputHandler.RawSecondaryAttackDirectionInput;
            _aimAngle = Mathf.Atan2(_ropeDirectionInput.y, _ropeDirectionInput.x);

            SetCrosshairPosition();
        }
    }

    public override void Exit()
    {
        base.Exit();

        RopeAttached = true;
        IsAiming = false;
        IsHoldingRope = false;
    }

    private void SetCrosshairPosition()
    {
        if (_aimAngle < 0f)
        {
            _aimAngle = Mathf.PI * 2 + _aimAngle;
        }
        AimDirection = Quaternion.Euler(0, 0, _aimAngle * Mathf.Rad2Deg) * Vector2.right;

        if (!Player.Crosshair.gameObject.activeInHierarchy)
        {
            Player.Crosshair.gameObject.SetActive(true);
        }

        Player.RopeHingeAnchor.gameObject.SetActive(false);

        float x = PlayerPosition.x + PlayerData.ropeCrosshairOffset * Mathf.Cos(_aimAngle);
        float y = PlayerPosition.y + PlayerData.ropeCrosshairOffset * Mathf.Sin(_aimAngle);

        _crossHairPosition = new Vector3(x, y, 0);
        Player.Crosshair.position = _crossHairPosition;
    }

}