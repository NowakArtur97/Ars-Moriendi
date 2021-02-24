public class Ogre_DamageState : DamageState
{
    private Ogre _ogre;

    public Ogre_DamageState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DamageState stateData, Ogre ogre)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _ogre = ogre;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (_ogre.StatsManager.IsStunned && FiniteStateMachine.CurrentState != _ogre.StunState)
            {
                FiniteStateMachine.ChangeState(_ogre.StunState);
            }
            else if (IsPlayerInMaxAgroRange)
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