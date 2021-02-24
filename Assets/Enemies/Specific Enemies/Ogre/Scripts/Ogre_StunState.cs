public class Ogre_StunState : StunState
{
    private Ogre _ogre;

    public Ogre_StunState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_StunState stateData, Ogre ogre)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _ogre = ogre;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsStunTimeOver)
        {
            if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_ogre.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_ogre.IdleState);
            }
        }
    }
}

