using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerOnRopeState_Move : PlayerOnRopeState
{
    private int _xInput;
    private bool _distanceSet;

    public PlayerOnRopeState_Move(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

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

                Player.InputHandler.UseSecondaryAttackInputStop();
            }
            else
            {
                _xInput = Player.InputHandler.NormalizedInputX;

                Player.CheckIfShouldFlip(_xInput);

                PlayerPosition = Player.transform.position;

                WrapRopeAroundObjects();
                UpdateRopePositions();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        RopeAttached = false;
        IsAiming = false;
        IsHoldingRope = false;
    }

    private void UpdateRopePositions()
    {
        // plus one for the Player position
        Player.MyRopeLineRenderer.positionCount = RopePositions.Count + 1;

        for (int i = Player.MyRopeLineRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != Player.MyRopeLineRenderer.positionCount - 1)
            {
                Player.MyRopeLineRenderer.SetPosition(i, RopePositions[i]);

                if (i == RopePositions.Count - 1 || RopePositions.Count == 1)
                {
                    Vector2 ropePosition = RopePositions[RopePositions.Count - 1];

                    if (RopePositions.Count == 1)
                    {
                        Player.RopeHingeAnchorRigidbody.transform.position = ropePosition;

                        if (!_distanceSet)
                        {
                            Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, ropePosition);
                            _distanceSet = true;
                        }
                    }
                    else
                    {
                        Player.RopeHingeAnchorRigidbody.transform.position = ropePosition;

                        if (!_distanceSet)
                        {
                            Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, ropePosition);
                            _distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == RopePositions.IndexOf(RopePositions.Last()))
                {
                    Vector2 ropePosition = RopePositions.Last();

                    Player.RopeHingeAnchorRigidbody.transform.position = ropePosition;

                    if (!_distanceSet)
                    {
                        Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, ropePosition);
                        _distanceSet = true;
                    }
                }
            }
            else
            {
                Player.MyRopeLineRenderer.SetPosition(i, PlayerPosition);
            }
        }
    }

    private void WrapRopeAroundObjects()
    {
        if (RopePositions.Count > 0)
        {
            Vector2 lastRopePoint = RopePositions.Last();
            RaycastHit2D playerToCurrentNextHit = Physics2D.Raycast(PlayerPosition, (lastRopePoint - PlayerPosition).normalized,
                Vector2.Distance(PlayerPosition, lastRopePoint) - 0.1f, PlayerData.whatCanYouAttachTo);

            if (playerToCurrentNextHit != null)
            {
                CompositeCollider2D platformsCollider = playerToCurrentNextHit.collider as CompositeCollider2D;

                if (platformsCollider != null)
                {
                    Vector2 closestPointToHit = platformsCollider.bounds.ClosestPoint(playerToCurrentNextHit.point);

                    if (WrapPointsLookup.ContainsKey(closestPointToHit))
                    {
                        Player.OnRopeStateFinish.ResetRope();
                    }
                    else
                    {
                        RopePositions.Add(closestPointToHit);
                        WrapPointsLookup.Add(closestPointToHit, 0);
                        _distanceSet = false;
                    }
                }
            }
        }
    }
}
