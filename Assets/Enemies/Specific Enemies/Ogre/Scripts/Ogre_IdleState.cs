public class Ogre_IdleState : IdleState
{
    private Ogre _ogre;

    public Ogre_IdleState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_IdleState stateData, Ogre ogre)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _ogre = ogre;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsPlayerInMaxAgroRange)
        {
            ShouldFlipAfterIdle(false);
            FiniteStateMachine.ChangeState(_ogre.PlayerDetectedState);
        }
        else if (IsIdleTimeOver)
        {
            ShouldFlipAfterIdle(!IsDetectingLedge || IsDetectingWall);
            FiniteStateMachine.ChangeState(_ogre.MoveState);
        }
    }
}
