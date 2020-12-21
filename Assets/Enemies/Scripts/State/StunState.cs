using UnityEngine;

public abstract class StunState : State
{
    protected D_StunState StateData;

    protected bool IsStunTimeOver;
    protected bool IsMovementStopped;

    protected bool IsGrounded;

    protected bool ShouldPerformCloseRangeAction;
    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
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

        Entity.SetVelocity(StateData.stunKnockbackSpeed, StateData.stunKnockbackAngle, Entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();

        Entity.ResetStunResistance();
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

            Entity.SetVelocity(0.0f);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Entity.CheckGround;

        ShouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro;
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInLongRangeAction();
    }
}
