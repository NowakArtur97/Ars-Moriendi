using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _holdPosition;

    public PlayerWallGrabState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, D_PlayerData PlayerData, string _animationBoolName) : base(Player, FiniteStateMachine, PlayerData, _animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _holdPosition = Player.transform.position;

        HoldPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            HoldPosition();

            if (YInput > 0)
            {
                FiniteStateMachine.ChangeState(Player.WallClimbState);
            }
            else if (YInput < 0)
            {
                FiniteStateMachine.ChangeState(Player.WallSlideState);
            }
        }
    }

    private void HoldPosition()
    {
        Player.transform.position = _holdPosition;

        Player.SetVelocityX(0.0f);
        Player.SetVelocityY(0.0f);
    }
}
