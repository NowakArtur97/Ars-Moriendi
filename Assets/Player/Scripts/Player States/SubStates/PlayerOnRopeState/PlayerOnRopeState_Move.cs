using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerOnRopeState_Move : PlayerOnRopeState
{
    private int _xInput;
    private int _yInput;
    private bool _distanceSet;
    private Vector2 _ropeHook;

    public List<Vector2> RopePositions { get; private set; }
    private Dictionary<Vector2, int> _wrapPointsLookup;

    public PlayerOnRopeState_Move(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerOnRopeState onRopeStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, onRopeStateData)
    {
        RopePositions = new List<Vector2>();
        _wrapPointsLookup = new Dictionary<Vector2, int>();
    }

    public override void Exit()
    {
        base.Enter();

        RopePositions.Clear();
        _wrapPointsLookup.Clear();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (RopeInputStop)
            {
                Player.InputHandler.UseSecondaryAttackInputStop();

                Player.FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateFinish);
            }
            else
            {
                _xInput = Player.InputHandler.NormalizedInputX;
                _yInput = Player.InputHandler.NormalizedInputY;

                Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);
                Player.MyAnmator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));

                Player.CheckIfShouldFlip(_xInput);

                PlayerPosition = Player.AliveGameObject.transform.position;

                if (IsGrounded)
                {
                    Player.SetVelocityZero();
                }

                MoveOnRope();
                ClimbRope();
                WrapRopeAroundObjects();
                UnwrapRope();
                UpdateRopePositions();
            }
        }
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
        if (RopePositions.Count == 0)
        {
            return;
        }

        Vector2 lastRopePoint = RopePositions.Last();
        RaycastHit2D playerToCurrentNextHit = Physics2D.Raycast(PlayerPosition, (lastRopePoint - PlayerPosition).normalized,
            Vector2.Distance(PlayerPosition, lastRopePoint) - 0.01f, OnRopeStateData.whatCanYouAttachTo);

        if (playerToCurrentNextHit)
        {
            CompositeCollider2D platformsCollider = playerToCurrentNextHit.collider as CompositeCollider2D;

            if (platformsCollider != null)
            {
                Vector2 closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, platformsCollider);

                if (_wrapPointsLookup.ContainsKey(closestPointToHit))
                {
                    Player.OnRopeStateFinish.ResetRope();
                }
                else
                {
                    RopePositions.Add(closestPointToHit);
                    _wrapPointsLookup.Add(closestPointToHit, 0);
                    _distanceSet = false;
                }
            }
        }
    }

    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, CompositeCollider2D platformsCollider)
    {
        List<Vector2> verts = GetVertsFromCompositeCollider2D(platformsCollider);

        Dictionary<float, Vector2> distanceDictionary = verts.ToDictionary<Vector2, float, Vector2>(
           position => Vector2.Distance(hit.point, platformsCollider.transform.TransformPoint(position)),
           position => platformsCollider.transform.TransformPoint(position));

        IOrderedEnumerable<KeyValuePair<float, Vector2>> orderedDictionary = distanceDictionary.OrderBy(e => e.Key);

        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }

    private void UnwrapRope()
    {
        if (RopePositions.Count <= 1)
        {
            return;
        }

        int anchorIndex = RopePositions.Count - 2;
        int hingeIndex = RopePositions.Count - 1;
        Vector2 anchorPosition = RopePositions[anchorIndex];
        Vector2 hingePosition = RopePositions[hingeIndex];
        Vector2 hingeDir = hingePosition - anchorPosition;
        float hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
        Vector2 playerDir = PlayerPosition - anchorPosition;
        float playerAngle = Vector2.Angle(anchorPosition, playerDir);

        int playerPositionIndicator = _wrapPointsLookup[hingePosition];
        int playerPositionIndicatorHelper = playerAngle < hingeAngle ? 1 : -1;

        if (playerPositionIndicator == playerPositionIndicatorHelper)
        {
            UnwrapRopePosition(anchorIndex, hingeIndex);
        }
        else
        {
            _wrapPointsLookup[hingePosition] = -playerPositionIndicatorHelper;
        }
    }

    private void UnwrapRopePosition(int anchorIndex, int hingeIndex)
    {
        Vector2 newAnchorPosition = RopePositions[anchorIndex];
        _wrapPointsLookup.Remove(RopePositions[hingeIndex]);
        RopePositions.RemoveAt(hingeIndex);

        Player.RopeHingeAnchorRigidbody.transform.position = newAnchorPosition;

        Player.RopeJoint.distance = Vector2.Distance(PlayerPosition, newAnchorPosition);
        _distanceSet = true;
    }

    private void MoveOnRope()
    {
        if (_xInput != 0 && !IsGrounded)
        {
            Vector2 playerToHookDirection = (_ropeHook - PlayerPosition).normalized;

            Vector2 perpendicularDirection;
            _ropeHook = RopePositions.Last();

            if (_xInput < 0)
            {
                perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);

                //Vector2 leftPerpendicularPosition = PlayerPosition - perpendicularDirection;
                //Debug.DrawLine(PlayerPosition, leftPerpendicularPosition, Color.green, 0f);
            }
            else
            {
                perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);

                //Vector2 rightPerpendicularPosition = PlayerPosition + perpendicularDirection;
                //Debug.DrawLine(PlayerPosition, rightPerpendicularPosition, Color.green, 0f);
            }

            Vector2 swingforce = perpendicularDirection * OnRopeStateData.ropeSwigForce;
            Player.AddForce(swingforce, ForceMode2D.Force);
        }
    }

    private void ClimbRope()
    {
        if (_yInput < 0 && Player.RopeJoint.distance < OnRopeStateData.ropeMaxCastDistance)
        {
            Player.RopeJoint.distance += Time.deltaTime * OnRopeStateData.ropeClimbingSpeed;
        }
        else if (_yInput > 0)
        {
            Player.RopeJoint.distance -= Time.deltaTime * OnRopeStateData.ropeClimbingSpeed;
        }
    }

    private static List<Vector2> GetVertsFromCompositeCollider2D(CompositeCollider2D platformsCollider)
    {
        List<Vector2> verts = new List<Vector2>();

        for (int i = 0; i < platformsCollider.pathCount; i++)
        {
            Vector2[] pathVerts = new Vector2[platformsCollider.GetPathPointCount(i)];
            platformsCollider.GetPath(i, pathVerts);
            verts.AddRange(pathVerts);
        }

        return verts;
    }

    public void AddRopePosition(Vector2 point) => RopePositions.Add(point);

    public void AddWrapPointsLookup(Vector2 point, int directionIndicator) => _wrapPointsLookup.Add(point, directionIndicator);
}
