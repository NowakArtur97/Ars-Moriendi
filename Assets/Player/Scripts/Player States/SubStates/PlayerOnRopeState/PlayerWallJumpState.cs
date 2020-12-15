using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private D_PlayerWallJumpState _wallJumpStateData;

    private int _wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerWallJumpState wallJumpStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _wallJumpStateData = wallJumpStateData;
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocity(_wallJumpStateData.wallJumpVelocity, _wallJumpStateData.wallJumpAngle, _wallJumpDirection);
        Player.InputHandler.UseJumpInput();
        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.CheckIfShouldFlip(_wallJumpDirection);
        Player.JumpState.DecreaseAmountOfJumps();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.MyAnmator.SetFloat("xVelocity", Player.CurrentVelocity.x);
        Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);

        if (Time.time >= StartTime + _wallJumpStateData.wallJumpTime)
        {
            IsAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall) => _wallJumpDirection = isTouchingWall ? -Player.FacingDirection : Player.FacingDirection;
}
