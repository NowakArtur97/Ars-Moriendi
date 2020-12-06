using UnityEngine;

public class PlayerOnRopeState_Attach : PlayerOnRopeState
{
    public PlayerOnRopeState_Attach(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        PlayerPosition = Player.transform.position;

        AttachRope();
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
            PlayerPosition = Player.transform.position;
            AddForceAfterRopeAttaching(hit.point);

            if (!RopePositions.Contains(hit.point))
            {
                RopePositions.Add(hit.point);
                WrapPointsLookup.Add(hit.point, 0);

                Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, hit.point);
                Player.RopeJoint.enabled = true;
                Player.RopeHingeAnchorSpriteRenderer.enabled = true;
            }
        }
        else
        {
            RopeAttached = false;
            IsAiming = false;
            IsHoldingRope = false;

            Player.RopeJoint.enabled = false;
            Player.RopeHingeAnchorSpriteRenderer.enabled = false;
        }
    }

    private void AddForceAfterRopeAttaching(Vector2 hitPoint)
    {
        var relativePoint = Player.transform.InverseTransformPoint(hitPoint).x;
        int forceDirection = relativePoint > 0 ? 1 : -1;

        Vector2 attachedRopeForce = new Vector2(PlayerData.attachedRopeForce.x * forceDirection, PlayerData.attachedRopeForce.y);
        Player.AddForce(attachedRopeForce, ForceMode2D.Force);
    }
}
