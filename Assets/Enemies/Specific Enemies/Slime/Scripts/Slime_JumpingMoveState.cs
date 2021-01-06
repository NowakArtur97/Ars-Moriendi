public class Slime_JumpingMoveState : JumpingMoveState
{
    private Slime _slime;

    public Slime_JumpingMoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_JumpingMoveState stateData, Slime slime)
       : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_slime.PlayerDetectedState);
        }
        else if (IsAnimationFinished)
        {
            _slime.IdleState.SetFlipAfterIdle(!IsDetectingLedge || IsDetectingWall);
            FiniteStateMachine.ChangeState(_slime.IdleState);
        }
    }
}
