public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime _slime;

    public Slime_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_PlayerDetectedState stateData, Slime slime)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(_slime.MeleeAttackState);
            }
            else if (ShouldPerformLongRangeAction && IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_slime.AreaAttackState);
            }
            else if (!IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_slime.IdleState);
            }
        }
    }
}
