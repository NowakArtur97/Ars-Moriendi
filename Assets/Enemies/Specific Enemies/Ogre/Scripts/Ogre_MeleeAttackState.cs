using UnityEngine;

public class Ogre_MeleeAttackState : MeleeAttackState
{
    private Ogre _ogre;

    public Ogre_MeleeAttackState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, Ogre ogre) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
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
