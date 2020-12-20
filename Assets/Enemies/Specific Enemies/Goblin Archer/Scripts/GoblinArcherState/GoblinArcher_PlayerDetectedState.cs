using UnityEngine;

public class GoblinArcher_PlayerDetectedState : PlayerDetectedState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerDetectedState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.goblinArcher = goblinArcher;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldPerformCloseRangeAction && isPlayerInMinAgroRange)
        {
            if (Time.time >= goblinArcher.dodgeState.startTime + goblinArcher.dodgeStateData.dodgeCooldwon)
            {
                FiniteStateMachine.ChangeState(goblinArcher.dodgeState);
            }
            else
            {
                FiniteStateMachine.ChangeState(goblinArcher.meleeAttackState);
            }
        }
        else if (shouldPerformLongRangeAction && isPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(goblinArcher.rangedAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            Entity.Flip();
            FiniteStateMachine.ChangeState(goblinArcher.moveState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
        }
    }
}
