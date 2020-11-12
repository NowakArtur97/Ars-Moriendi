using System.Collections.Generic;
using UnityEngine;

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
    private List<Vector2> _ropePositions;

    public PlayerMoveOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
        _ropePositions = new List<Vector2>();
    }

    public override void Enter()
    {
        base.Enter();

        _playerPosition = Player.transform.position;
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

            if (_aimAngle < 0f)
            {
                _aimAngle = Mathf.PI * 2 + _aimAngle;
            }
            _aimDirection = Quaternion.Euler(0, 0, _aimAngle * Mathf.Rad2Deg) * Vector2.right;
            _playerPosition = Player.transform.position;

            if (_ropeInputStop)
            {
                Player.Crosshair.gameObject.SetActive(false);
                ResetRope();

                // TODO: Exit state
                //IsAbilityDone = true;
            }
            else if (!_ropeAttached && !_ropeInputStop)
            {
                AttachRope();
            }
            else if (!_ropeAttached)
            {
                SetCrosshairPosition();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        _ropeAttached = false;
    }

    private void AttachRope()
    {
        if (_ropeAttached)
        {
            return;
        }

        Player.MyRopeLineRenderer.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(_playerPosition, _aimDirection, PlayerData.ropeMaxCastDistance, PlayerData.whatCanYouAttachTo);

        if (hit.collider != null)
        {
            _ropeAttached = true;

            if (!_ropePositions.Contains(hit.point))
            {
                Player.MyRigidbody.AddForce(_aimDirection * PlayerData.ropeStartingVelocity);

                _ropePositions.Add(hit.point);
                Player.RopeJoint.distance = Vector2.Distance(_playerPosition.normalized, hit.point);
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

        float x = Player.transform.position.x + PlayerData.ropeCrosshairOffset * Mathf.Cos(_aimAngle);
        float y = Player.transform.position.y + PlayerData.ropeCrosshairOffset * Mathf.Sin(_aimAngle);

        _crossHairPosition = new Vector3(x, y, 0);
        Player.Crosshair.position = _crossHairPosition;
    }
}
