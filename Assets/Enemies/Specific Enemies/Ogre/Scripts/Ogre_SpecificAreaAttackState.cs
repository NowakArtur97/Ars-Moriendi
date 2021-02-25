using UnityEngine;

public class Ogre_SpecificAreaAttackState : SpecificAreaAttackState
{
    private Ogre _ogre;

    public Ogre_SpecificAreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition, D_SpecificAreaAttackState stateData, Ogre ogre) : base(finiteStateMachine, enemy, animationBoolName, attackPosition, stateData)
    {
        _ogre = ogre;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_ogre.PlayerDetectedState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_ogre.LookForPlayerState);
            }
        }
    }
}
