public class Boar_ChargeState : ChargeState
{
    private Boar _boar;

    public Boar_ChargeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_ChargeState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (ShouldPerformCloseRangeAction)
        {
            FiniteStateMachine.ChangeState(_boar.MeleeAttackState);
        }
        else if (HasDetectedObstacle())
        {
            FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
        }
        else if (IsDetectingPlayerAbove)
        {
            FiniteStateMachine.ChangeState(_boar.SlowDownState);
        }
        else if (IsPlayerInMinAgroRange)
        {
            FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
        }
    }

    private bool HasDetectedObstacle()
    {
        return !IsDetectingLedge || IsDetectingWall;
    }
}
