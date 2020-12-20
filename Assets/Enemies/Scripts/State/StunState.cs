using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;

    protected bool isStunTimeOver;
    protected bool isMovementStopped;

    protected bool isGrounded;

    protected bool shouldPerformCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    public StunState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_StunState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementStopped = false;

        shouldPerformCloseRangeAction = false;
        isPlayerInMinAgroRange = false;

        Entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, Entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();

        Entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;

            Entity.SetVelocity(0.0f);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = Entity.CheckGround();

        shouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = Entity.CheckIfPlayerInLongRangeAction();
    }
}
