using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState_Attach : PlayerOnRopeState
{
    public PlayerOnRopeState_Attach(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("PlayerOnRopeState_Attach");

        Player.InputHandler.UseSecondaryAttackInputStop();

        RopeAttached = true;
        IsAiming = false;
        IsHoldingRope = true;

        AttachRope();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (RopeInput)
            {
                RopeAttached = true;
                IsHoldingRope = true;

                Player.InputHandler.UseSecondaryAttackInput();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        RopeAttached = true;
        IsAiming = false;
        IsHoldingRope = true;
    }

    private void AttachRope()
    {
        Player.Crosshair.gameObject.SetActive(false);
        Player.MyRopeLineRenderer.gameObject.SetActive(true);
        Player.RopeHingeAnchor.gameObject.SetActive(true);

        RaycastHit2D hit = Physics2D.Raycast(PlayerPosition, AimDirection, PlayerData.ropeMaxCastDistance, PlayerData.whatCanYouAttachTo);

        if (hit.collider != null)
        {
            RopeAttached = true;

            if (!RopePositions.Contains(hit.point))
            {
                RopePositions.Add(hit.point);
                Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, hit.point);
                Player.RopeJoint.enabled = true;
                Player.RopeHingeAnchorSpriteRenderer.enabled = true;
            }
        }
        else
        {
            RopeAttached = false;
            Player.RopeJoint.enabled = false;
            Player.RopeHingeAnchorSpriteRenderer.enabled = false;
        }
    }
}
