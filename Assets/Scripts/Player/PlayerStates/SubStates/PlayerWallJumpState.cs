using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, wallJumpDirection);
        Player.InputHandler.UseJumpInput();
        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.CheckIfShouldFlip(wallJumpDirection);
        Player.JumpState.DecreaseAmountOfJumps();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.MyAnmator.SetFloat("xVelocity", Player.CurrentVelocity.x);
        Player.MyAnmator.SetFloat("yVelocity", Player.CurrentVelocity.y);

        if (Time.time >= StartTime + PlayerData.wallJumpTime)
        {
            IsAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall) => wallJumpDirection = isTouchingWall ? -Player.FacingDirection : Player.FacingDirection;
}
