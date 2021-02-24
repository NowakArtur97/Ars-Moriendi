public class Ogre_MoveState : MoveState
{
    private Ogre _ogre;

    public Ogre_MoveState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_MoveState stateData, Ogre ogre)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
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
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            _ogre.IdleState.ShouldFlipAfterIdle(true);
            FiniteStateMachine.ChangeState(_ogre.IdleState);
        }
    }
}
