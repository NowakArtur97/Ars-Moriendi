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
                finiteStateMachine.ChangeState(goblinArcher.dodgeState);
            }
            else
            {
                finiteStateMachine.ChangeState(goblinArcher.meleeAttackState);
            }
        }
        else if (shouldPerformLongRangeAction && isPlayerInMaxAgroRange)
        {
            finiteStateMachine.ChangeState(goblinArcher.rangedAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            entity.Flip();
            finiteStateMachine.ChangeState(goblinArcher.moveState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            finiteStateMachine.ChangeState(goblinArcher.lookForPlayerState);
        }
    }
}
