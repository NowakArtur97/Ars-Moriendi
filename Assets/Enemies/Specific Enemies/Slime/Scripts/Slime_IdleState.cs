public class Slime_IdleState : IdleState
{
    private Slime _slime;

    public Slime_IdleState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_IdleState stateData, Slime slime)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_slime.PlayerDetectedState);
        }
        else if (IsIdleTimeOver)
        {
            FiniteStateMachine.ChangeState(_slime.JumpingMoveState);
        }
    }
}
