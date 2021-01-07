public class Boar_PlayerDetectedState : PlayerDetectedState
{
    private Boar _boar;

    public Boar_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_PlayerDetectedState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsDetectingLedge || IsDetectingWall)
        {
            Enemy.Flip();
            FiniteStateMachine.ChangeState(_boar.MoveState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
        }
        else if (IsPlayerInMinAgroRange && ShouldPerformCloseRangeAction)
        {
            FiniteStateMachine.ChangeState(_boar.MeleeAttackState);
        }
        else if (IsPlayerInMaxAgroRange && ShouldPerformLongRangeAction)
        {
            FiniteStateMachine.ChangeState(_boar.ChargeState);
        }
    }
}
