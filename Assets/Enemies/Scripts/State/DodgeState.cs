using UnityEngine;

public abstract class DodgeState : State
{
    protected D_DodgeState StateData;

    protected bool ShouldPerformCloseRangeAction;
    protected bool ShouldPerformLongRangeAction;
    protected bool IsPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool IsDodgeTimeOver;

    protected bool IsGrounded;

    public DodgeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DodgeState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsDodgeTimeOver = false;

        Entity.SetVelocity(StateData.dodgeSpeed, StateData.dodgeAngle, -Entity.FacingDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.dodgeTime && IsGrounded)
        {
            IsDodgeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro();

        ShouldPerformCloseRangeAction = Entity.CheckIfPlayerInCloseRangeAction();
        ShouldPerformLongRangeAction = Entity.CheckIfPlayerInLongRangeAction();

        IsGrounded = Entity.CheckIfGrounded();
    }
}
