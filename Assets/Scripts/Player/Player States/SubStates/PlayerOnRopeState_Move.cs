using System.Linq;
using UnityEngine;

public class PlayerOnRopeState_Move : PlayerOnRopeState
{
    private int _xInput;
    private int _yInput;
    private bool _distanceSet;
    private Vector2 _ropeHook;

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
                _yInput = Player.InputHandler.NormalizedInputX;

                Player.CheckIfShouldFlip(_xInput);

                PlayerPosition = Player.transform.position;

                MoveOnRope();
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

            if (playerToCurrentNextHit)
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

    private void MoveOnRope()
    {
        if (_xInput != 0 && _yInput != 0)
        {
            Vector2 playerToHookDirection = (_ropeHook - PlayerPosition).normalized;

            Vector2 perpendicularDirection;
            _ropeHook = RopePositions.Last();

            if (_xInput < 0)
            {
                perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                Vector2 leftPerpendicularPosition = PlayerPosition - perpendicularDirection * -2f;

                Debug.DrawLine(PlayerPosition, leftPerpendicularPosition, Color.green, 0f);
            }
            else
            {
                perpendicularDirection = new Vector2(playerToHookDirection.y, playerToHookDirection.x);
                Vector2 rightPerpendicularPosition = PlayerPosition + perpendicularDirection * -2f;

                Debug.DrawLine(PlayerPosition, rightPerpendicularPosition, Color.green, 0f);
            }

            Vector2 force = perpendicularDirection * PlayerData.ropeSwigForce;
            Player.AddForce(force, ForceMode2D.Force);
        }
    }
}
