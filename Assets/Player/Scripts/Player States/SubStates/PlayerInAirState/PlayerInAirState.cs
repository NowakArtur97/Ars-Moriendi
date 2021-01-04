﻿using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private D_PlayerInAirState _inAirStateData;

    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;
    private bool _dashInput;
    private bool _primaryAttackInput;
    private bool _secondaryAttackInput;

    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;
    private bool _previousIsTouchingWall;
    private bool _prevoiusIsBackTouchingWall;
    private bool _isBackTouchingWall;
    private bool _isJumping;

    private bool _coyoteTime;
    private bool _wallJumpCoyoteTime;
    private float _startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerInAirState inAirStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _inAirStateData = inAirStateData;
    }

    public override void Exit()
    {
        base.Exit();

        _previousIsTouchingWall = false;
        _prevoiusIsBackTouchingWall = false;
        _isTouchingWall = false;
        _isBackTouchingWall = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        _xInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _grabInput = Player.InputHandler.GrabInput;
        _dashInput = Player.InputHandler.DashInput;
        _primaryAttackInput = Player.InputHandler.PrimaryInput;
        _secondaryAttackInput = Player.InputHandler.SecondaryInput;

        CheckJumpHeightMultiplier();

        if (_primaryAttackInput)
        {
            FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState01);
        }
        else if (_secondaryAttackInput && Player.SkillManager.GetCurrentSkill().CanUseAbility())
        {
            FiniteStateMachine.ChangeCurrentState(Player.SkillManager.GetCurrentSkill());
        }
        else if (_isTouchingWall && _xInput == Player.FacingDirection && Player.CurrentVelocity.y <= 0)
        {
            FiniteStateMachine.ChangeCurrentState(Player.WallSlideState);
        }
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
        {
            FiniteStateMachine.ChangeCurrentState(Player.LedgeClimbState);
        }
        else if (_jumpInput && (_isTouchingWall || _isBackTouchingWall || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            _isTouchingWall = Player.CheckIfTouchingWall();
            Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            FiniteStateMachine.ChangeCurrentState(Player.WallJumpState);
        }
        else if (_jumpInput && Player.JumpState.CanUseAbility())
        {
            FiniteStateMachine.ChangeCurrentState(Player.JumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            FiniteStateMachine.ChangeCurrentState(Player.WallGrabState);
        }
        else if (_isGrounded & Player.CurrentVelocity.y < 0.01f)
        {
            FiniteStateMachine.ChangeCurrentState(Player.LandState);
        }
        else if (_dashInput && Player.DashState.CanUseAbility())
        {
            FiniteStateMachine.ChangeCurrentState(Player.DashState);
        }
        else
        {
            Player.CheckIfShouldFlip(_xInput);
            Player.SetVelocityX(_inAirStateData.movementVelocity * _xInput);

            Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.MyAnmator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _previousIsTouchingWall = _isTouchingWall;
        _prevoiusIsBackTouchingWall = _isBackTouchingWall;

        _isGrounded = Player.CheckIfGrounded();
        _isTouchingWall = Player.CheckIfTouchingWall();
        _isBackTouchingWall = Player.CheckIfBackIsTouchingWall();
        _isTouchingLedge = Player.CheckIfTouchingLedge();

        if (_isTouchingWall && !_isTouchingLedge)
        {
            Player.LedgeClimbState.SetDetectedPosition(Player.AliveGameObject.transform.position);
        }

        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isBackTouchingWall && (_previousIsTouchingWall || _prevoiusIsBackTouchingWall))
        {
            StartWallJumpCoyoteTime();
        }
    }

    private void CheckJumpHeightMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * _inAirStateData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Player.CurrentVelocity.y <= 0)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time >= StartTime + _inAirStateData.coyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumps();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (_wallJumpCoyoteTime && Time.time >= _startWallJumpCoyoteTime + _inAirStateData.coyoteTime)
        {
            _wallJumpCoyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumps();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumping() => _isJumping = true;

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;
}
