using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;

    private Vector2 _startPosition;
    private Vector2 _stopPosition;

    private bool _isHanging;
    private bool _isClimbing;

    private int _xInput;
    private int _yInput;
    private bool _jumpInput;

    public PlayerLedgeClimbState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName)
        : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityZero();
        Player.transform.position = _detectedPosition;

        _cornerPosition = Player.DetermineCornerPosition();
        _startPosition.Set(_cornerPosition.x - (Player.FacingDirection * PlayerData.ledgeClimbStartOffset.x),
            _cornerPosition.y - PlayerData.ledgeClimbStartOffset.y);
        _stopPosition.Set(_cornerPosition.x + (Player.FacingDirection * PlayerData.ledgeClimbStopOffset.x),
            _cornerPosition.y + PlayerData.ledgeClimbStopOffset.y);

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if (_isClimbing)
        {
            Player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            Player.FiniteStateMachine.ChangeState(Player.IdleState);
        }
        else
        {
            _xInput = Player.InputHandler.NormalizedInputX;
            _yInput = Player.InputHandler.NormalizedInputY;
            _jumpInput = Player.InputHandler.JumpInput;

            Player.SetVelocityZero();
            Player.transform.position = _startPosition;

            if ((_xInput == Player.FacingDirection || _yInput == 1) && _isHanging && !_isClimbing)
            {
                _isClimbing = true;
                Player.MyAnmator.SetBool("climbLedge", true);
            }
            else if ((_yInput == -1 || _xInput == -Player.FacingDirection) && _isHanging && !_isClimbing)
            {
                Player.FiniteStateMachine.ChangeState(Player.InAirState);
            }
            else if (_jumpInput && !_isClimbing)
            {
                Player.WallJumpState.DetermineWallJumpDirection(true);
                Player.FiniteStateMachine.ChangeState(Player.WallJumpState);
            }
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        Player.MyAnmator.SetBool("climbLedge", false);
    }

    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;
}
