// TODO: Remove state
public class Slime_MoveState : MoveState
{
    private Slime _slime;

    public Slime_MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData, Slime slime)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsDetectingLedge || IsDetectingWall)
        {
            _slime.IdleState.SetFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(_slime.IdleState);
        }
    }
}
