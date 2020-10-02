using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int _xInput;
    private bool _jumpInput;

    private bool _isGrounded;

    public PlayerInAirState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;

        if (_isGrounded & Player.CurrentVelocity.y < 0.01f)
        {
            FiniteStateMachine.ChangeState(Player.LandState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            FiniteStateMachine.ChangeState(Player.JumpState);
        }
        else
        {
            Player.CheckIfShouldFlip(_xInput);
            Player.SetVelocityX(PlayerData.movementVelocity * _xInput);

            Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.MyAnmator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
    }
}
