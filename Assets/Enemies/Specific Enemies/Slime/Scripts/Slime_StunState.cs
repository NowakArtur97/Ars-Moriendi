public class Slime_StunState : StunState
{
    private Slime _slime;

    public Slime_StunState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_StunState stateData, Slime slime)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsStunTimeOver)
        {
            if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_slime.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_slime.IdleState);
            }
        }
    }
}
