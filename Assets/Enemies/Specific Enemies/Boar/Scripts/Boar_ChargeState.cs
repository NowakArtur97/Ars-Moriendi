public class Boar_ChargeState : ChargeState
{
    private Boar _boar;

    public Boar_ChargeState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_ChargeState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMinAgroRange)
        {
            FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
        }
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
        }
        else if (IsDetectingPlayerAbove)
        {
            FiniteStateMachine.ChangeState(_boar.SlowDownState);
        }
    }
}
