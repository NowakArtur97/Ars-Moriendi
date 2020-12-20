using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;

    protected bool shouldPerformCloseRangeAction;
    protected bool shouldPerformLongRangeAction;
    protected bool isPlayerInMinAgroRange;
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

        isDodgeTimeOver = false;

        Entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -Entity.facingDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();

        shouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        shouldPerformLongRangeAction = Entity.CheckIfPlayerInLongRangeAction();

        isGrounded = Entity.CheckGround();
    }
}
