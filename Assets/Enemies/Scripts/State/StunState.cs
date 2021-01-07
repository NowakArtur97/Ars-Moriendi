using UnityEngine;

public abstract class StunState : EnemyState
{
    protected D_StunState StateData;

    protected bool IsStunTimeOver;
    protected bool IsMovementStopped;

    protected bool IsGrounded;

    protected bool ShouldPerformCloseRangeAction;
    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public StunState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_StunState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsStunTimeOver = false;
        IsMovementStopped = false;

        ShouldPerformCloseRangeAction = false;
        IsPlayerInMinAgroRange = false;

        Enemy.SetVelocity(StateData.stunKnockbackSpeed, StateData.stunKnockbackAngle, Enemy.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();

        Enemy.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.stunTime)
        {
            IsStunTimeOver = true;
        }

        if (IsGrounded && Time.time >= StartTime + StateData.stunKnockbackTime && !IsMovementStopped)
        {
            IsMovementStopped = true;

            Enemy.SetVelocity(0.0f);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Enemy.CheckIfGrounded();

        ShouldPerformCloseRangeAction = Enemy.CheckIfPlayerInCloseRangeAction();
        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInLongRangeAction();
    }
}
