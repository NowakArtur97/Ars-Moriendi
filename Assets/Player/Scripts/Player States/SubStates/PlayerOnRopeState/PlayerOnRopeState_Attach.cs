using UnityEngine;

public class PlayerOnRopeState_Attach : PlayerOnRopeState
{
    private Vector2 _aimDirection;

    public PlayerOnRopeState_Attach(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerOnRopeState onRopeStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, onRopeStateData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        PlayerPosition = Player.AliveGameObject.transform.position;

        AttachRope();
    }

    public override void Exit()
    {
        base.Exit();

        if (Player.StatsManager.IsStunned)
        {
            Player.OnRopeStateFinish.ResetRope();
        }
    }

    private void AttachRope()
    {
        Player.MyRopeLineRenderer.gameObject.SetActive(true);
        Player.RopeHingeAnchor.gameObject.SetActive(true);

        RaycastHit2D hit = Physics2D.Raycast(PlayerPosition, _aimDirection, OnRopeStateData.ropeMaxCastDistance, OnRopeStateData.whatCanYouAttachTo);

        if (hit.collider != null)
        {
            PlayerPosition = Player.AliveGameObject.transform.position;

            if (IsGrounded)
            {
                AddForceAfterRopeAttaching(hit.point);
            }

            if (!Player.OnRopeStateMove.RopePositions.Contains(hit.point))
            {
                Player.OnRopeStateMove.AddRopePosition(hit.point);
                Player.OnRopeStateMove.AddWrapPointsLookup(hit.point, 0);

                Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, hit.point);
                Player.RopeJoint.enabled = true;
                Player.RopeHingeAnchorSpriteRenderer.enabled = true;
            }

            Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateMove);
        }
        else
        {
            Player.RopeJoint.enabled = false;
            Player.RopeHingeAnchorSpriteRenderer.enabled = false;

            Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateFinish);
        }
    }

    private void AddForceAfterRopeAttaching(Vector2 hitPoint)
    {
        float relativePoint = Player.AliveGameObject.transform.InverseTransformPoint(hitPoint).x;
        int forceDirection = relativePoint > 0 ? 1 : -1;

        Vector2 attachedRopeForce = new Vector2(OnRopeStateData.attachedRopeForce.x * forceDirection, OnRopeStateData.attachedRopeForce.y);
        Player.AddForce(attachedRopeForce, ForceMode2D.Force);
    }

    public void SetAimDirection(Vector2 aimDirection) => _aimDirection = aimDirection;
}
