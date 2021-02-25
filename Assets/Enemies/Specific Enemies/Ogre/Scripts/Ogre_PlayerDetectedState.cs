public class Ogre_PlayerDetectedState : PlayerDetectedState
{
    private Ogre _ogre;

    public Ogre_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_PlayerDetectedState stateData, Ogre ogre)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _ogre = ogre;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
            {
                FiniteStateMachine.ChangeState(_ogre.MeleeAttackState);
            }
            else if (ShouldPerformLongRangeAction && IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_ogre.SpecificAreaAttackState);
            }
            else if (!IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_ogre.LookForPlayerState);
            }
        }
    }
}
