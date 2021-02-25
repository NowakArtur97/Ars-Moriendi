public class Ogre_LookForPlayerState : LookForPlayerState
{
    private Ogre _ogre;

    public Ogre_LookForPlayerState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_LookForPlayerState stateData,
        Ogre ogre) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _ogre = ogre;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_ogre.PlayerDetectedState);
        }
        else if (AreAllTurnsTimeDone)
        {
            FiniteStateMachine.ChangeState(_ogre.IdleState);
        }
    }
}
