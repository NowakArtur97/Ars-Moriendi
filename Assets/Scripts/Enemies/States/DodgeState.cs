using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;

    protected bool shouldPerformCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;

    protected bool isDodgeTimeOver;

    protected bool isGrounded;

    public DodgeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DodgeState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        shouldPerformCloseRangeAction = false;
        isPlayerInMaxAgroRange = false;

        isDodgeTimeOver = false;
        isGrounded = false;

        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMaxAgroRange = entity.CheckIfPlayerInMaxAgro();

        shouldPerformCloseRangeAction = entity.CheckIfPlayerInCloseRangeAction();

        isGrounded = entity.CheckGround();
    }
}
