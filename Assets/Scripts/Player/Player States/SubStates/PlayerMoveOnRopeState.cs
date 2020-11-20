using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMoveOnRopeState : PlayerAbilityState
{
    private int _xInput;
    private Vector2 _ropeDirectionInput;
    private float _aimAngle;
    private Vector2 _aimDirection;
    private bool _ropeInputStop;
    private Vector2 _playerPosition;
    private Vector3 _crossHairPosition;
    private bool _ropeAttached;
    private int _clickCount;
    private bool _distanceSet;
    private List<Vector2> _ropePositions;

    public PlayerMoveOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
        _ropePositions = new List<Vector2>();
    }

    public override void Enter()
    {
        base.Enter();

        _playerPosition = Player.transform.position;

        _clickCount = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            _xInput = Player.InputHandler.NormalizedInputX;
            _ropeDirectionInput = Player.InputHandler.RawSecondaryAttackDirectionInput;
            _ropeInputStop = Player.InputHandler.SecondaryAttackInputStop;
            _aimAngle = Mathf.Atan2(_ropeDirectionInput.y, _ropeDirectionInput.x);

            Player.SetVelocityX(PlayerData.movementVelocity * _xInput);

            if (_aimAngle < 0f)
            {
                _aimAngle = Mathf.PI * 2 + _aimAngle;
            }
            _aimDirection = Quaternion.Euler(0, 0, _aimAngle * Mathf.Rad2Deg) * Vector2.right;
            _playerPosition = Player.transform.position;

            if (_ropeInputStop)
            {
                _clickCount++;
            }

            if (!_ropeAttached && !_ropeInputStop)
            {
                SetCrosshairPosition();
            }
            else if (!_ropeAttached && _clickCount == 1)
            {
                Player.InputHandler.UseSecondaryAttackInputStop();

                AttachRope();
            }
            else if (_ropeAttached && _clickCount == 1)
            {
                Player.CheckIfShouldFlip(_xInput);

                UpdateRopePositions();
            }
            else if (_ropeAttached && _clickCount == 2)
            {
                Player.InputHandler.UseSecondaryAttackInputStop();

                ResetRope();

                IsAbilityDone = true;
            }
        }
    }

    private void AttachRope()
    {
        Player.Crosshair.gameObject.SetActive(false);
        Player.MyRopeLineRenderer.gameObject.SetActive(true);
        Player.RopeHingeAnchor.gameObject.SetActive(true);

        RaycastHit2D hit = Physics2D.Raycast(_playerPosition, _aimDirection, PlayerData.ropeMaxCastDistance, PlayerData.whatCanYouAttachTo);

        if (hit.collider != null)
        {
            _ropeAttached = true;

            if (!_ropePositions.Contains(hit.point))
            {
                _ropePositions.Add(hit.point);
                Player.RopeJoint.distance = Vector2.Distance(_playerPosition, hit.point);
                Player.RopeJoint.enabled = true;
                Player.RopeHingeAnchorSpriteRenderer.enabled = true;
            }
        }
        else
        {
            _ropeAttached = false;
            Player.RopeJoint.enabled = false;
            Player.RopeHingeAnchorSpriteRenderer.enabled = false;
        }
    }

    private void UpdateRopePositions()
    {
        // plus one for the Player position
        Player.MyRopeLineRenderer.positionCount = _ropePositions.Count + 1;

        for (int i = Player.MyRopeLineRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != Player.MyRopeLineRenderer.positionCount - 1)
            {
                Player.MyRopeLineRenderer.SetPosition(i, _ropePositions[i]);

                if (i == _ropePositions.Count - 1 || _ropePositions.Count == 1)
                {
                    Vector2 ropePosition = _ropePositions[_ropePositions.Count - 1];

                    if (_ropePositions.Count == 1)
                    {
                        Player.RopeHingeAnchorRigidbody.transform.position = ropePosition;

                        if (!_distanceSet)
                        {
                            Player.RopeJoint.distance = Vector2.Distance(_playerPosition, ropePosition);
                            _distanceSet = true;
                        }
                    }
                    else
                    {
                        Player.RopeHingeAnchorRigidbody.transform.position = ropePosition;

                        if (!_distanceSet)
                        {
                            Player.RopeJoint.distance = Vector2.Distance(_playerPosition, ropePosition);
                            _distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == _ropePositions.IndexOf(_ropePositions.Last()))
                {
                    Vector2 ropePosition = _ropePositions.Last();

                    Player.RopeHingeAnchorRigidbody.transform.position = ropePosition;

                    if (!_distanceSet)
                    {
                        Player.RopeJoint.distance = Vector2.Distance(_playerPosition, ropePosition);
                        _distanceSet = true;
                    }
                }
            }
            else
            {
                Player.MyRopeLineRenderer.SetPosition(i, _playerPosition);
            }
        }
    }

    private void ResetRope()
    {
        Player.RopeJoint.enabled = false;
        _ropeAttached = false;
        Player.RopeHingeAnchorSpriteRenderer.enabled = false;

        Player.MyRopeLineRenderer.positionCount = 2;
        Player.MyRopeLineRenderer.SetPosition(0, _playerPosition);
        Player.MyRopeLineRenderer.SetPosition(1, _playerPosition);
        _ropePositions.Clear();
    }

    private void SetCrosshairPosition()
    {
        if (!Player.Crosshair.gameObject.activeInHierarchy)
        {
            Player.Crosshair.gameObject.SetActive(true);
        }

        Player.RopeHingeAnchor.gameObject.SetActive(false);

        float x = _playerPosition.x + PlayerData.ropeCrosshairOffset * Mathf.Cos(_aimAngle);
        float y = _playerPosition.y + PlayerData.ropeCrosshairOffset * Mathf.Sin(_aimAngle);

        _crossHairPosition = new Vector3(x, y, 0);
        Player.Crosshair.position = _crossHairPosition;
    }
}
