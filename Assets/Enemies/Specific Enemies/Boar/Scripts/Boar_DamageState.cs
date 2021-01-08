using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar_DamageState : DamageState
{
    private Boar _boar;

    public Boar_DamageState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DamageState stateData,
        Boar goblinArcher) : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _boar = goblinArcher;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (_boar.StatsManager.IsDead)
            {
                FiniteStateMachine.ChangeState(_boar.DeadState);
            }
            else if (_boar.StatsManager.IsStunned)
            {
                FiniteStateMachine.ChangeState(_boar.StunState);
            }
            else if (IsPlayerInMaxAgroRange)
            {
                FiniteStateMachine.ChangeState(_boar.PlayerDetectedState);
            }
            else
            {
                _boar.LookForPlayerState.SetShouldTurnImmediately(_boar.LastDamageDirection == _boar.FacingDirection);
                FiniteStateMachine.ChangeState(_boar.LookForPlayerState);
            }
        }
    }
}
