public class Slime_JumpingMoveState : JumpingMoveState
{
    private Slime _slime;

    public Slime_JumpingMoveState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_JumpingMoveState stateData, Slime slime)
       : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            _slime.IdleState.ShouldFlipAfterIdle(!IsDetectingLedge || IsDetectingWall);
            FiniteStateMachine.ChangeState(_slime.IdleState);
        }
    }
}
