using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;

    private Vector2 _startPosition;
    private Vector2 _stopPositionPosition;

    public PlayerLedgeClimbState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, D_PlayerData PlayerData, string _animationBoolName) : base(Player, FiniteStateMachine, PlayerData, _animationBoolName)
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
        _stopPositionPosition.Set(_cornerPosition.x + (Player.FacingDirection * PlayerData.ledgeClimbStopOffset.x),
            _cornerPosition.y + PlayerData.ledgeClimbStopOffset.y);

        Player.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityZero();
        Player.transform.position = _startPosition;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public void SetDetectedPosition(Vector2 position) => _detectedPosition = position;
}
