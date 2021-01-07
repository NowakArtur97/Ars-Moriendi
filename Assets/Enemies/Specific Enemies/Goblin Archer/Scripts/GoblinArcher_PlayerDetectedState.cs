using UnityEngine;

public class GoblinArcher_PlayerDetectedState : PlayerDetectedState
{
    private GoblinArcher _goblinArcher;

    public GoblinArcher_PlayerDetectedState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_PlayerDetectedState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (ShouldPerformCloseRangeAction && IsPlayerInMinAgroRange)
        {
            if (Time.time >= _goblinArcher.DodgeState.LastDodgeTime + _goblinArcher._dodgeStateData.dodgeCooldwon)
            {
                _goblinArcher.DodgeState.ShouldFlipAfterDodge(IsDetectingWallBehind);
                _goblinArcher.DodgeState.ShouldDodgeInOppositeDirection(IsDetectingWallBehind);
                FiniteStateMachine.ChangeState(_goblinArcher.DodgeState);
            }
            else
            {
                FiniteStateMachine.ChangeState(_goblinArcher.MeleeAttackState);
            }
        }
        else if (ShouldPerformLongRangeAction && IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_goblinArcher.RangedAttackState);
        }
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            Enemy.Flip();
            FiniteStateMachine.ChangeState(_goblinArcher.MoveState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            FiniteStateMachine.ChangeState(_goblinArcher.LookForPlayerState);
        }
    }
}
