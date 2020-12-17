﻿using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    protected bool CrouchInput;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _dashInput;
    private bool _primaryAttackInput;
    private bool _secondaryAttackInput;

    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    public PlayerGroundedState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        CrouchInput = Player.InputHandler.CrouchInput;
        _jumpInput = Player.InputHandler.JumpInput;
        _grabInput = Player.InputHandler.GrabInput;
        _dashInput = Player.InputHandler.DashInput;
        _primaryAttackInput = Player.InputHandler.PrimaryAttackInput;
        _secondaryAttackInput = Player.InputHandler.SecondaryAttackInput;

        if (_primaryAttackInput)
        {
            FiniteStateMachine.ChangeCurrentState(Player.SwordAttackState01);
        }
        else if (_secondaryAttackInput) // && Player.FireArrowShotStateFinish.CheckIfCanShoot())
        {
            FiniteStateMachine.ChangeCurrentState(Player.OnRopeStateAim);
            // TO DO: Change attack type
            //FiniteStateMachine.ChangeState(Player.FireArrowShotStateStart);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            FiniteStateMachine.ChangeCurrentState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            FiniteStateMachine.ChangeCurrentState(Player.InAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            FiniteStateMachine.ChangeCurrentState(Player.WallGrabState);
        }
        else if (_dashInput && Player.DashState.CheckIfCanDash())
        {
            FiniteStateMachine.ChangeCurrentState(Player.DashState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
        _isTouchingWall = Player.CheckIfTouchingWall();
        _isTouchingLedge = Player.CheckIfTouchingLedge();
    }
}
