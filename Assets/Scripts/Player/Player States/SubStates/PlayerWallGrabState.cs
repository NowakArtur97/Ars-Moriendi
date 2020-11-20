using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _holdPosition;

    public PlayerWallGrabState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName)
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

        Player.SetVelocityZero();
    }
}
