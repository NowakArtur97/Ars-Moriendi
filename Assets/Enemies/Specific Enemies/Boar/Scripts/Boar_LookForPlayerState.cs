public class Boar_LookForPlayerState : LookForPlayerState
{
    private Boar boar;

    public Boar_LookForPlayerState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_LookForPlayerState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.boar = boar;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange || IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(boar.playerDetectedState);
        }
        else if (AreAllTurnsTimeDone)
        {
            FiniteStateMachine.ChangeState(boar.moveState);
        }
    }
}
