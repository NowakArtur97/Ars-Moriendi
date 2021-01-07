public class Boar_MoveState : MoveState
{
    private Boar _boar;

    public Boar_MoveState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_MoveState stateData, Boar boar) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
        }
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            _boar.IdleState.ShouldFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(_boar.IdleState);
        }
    }
}
