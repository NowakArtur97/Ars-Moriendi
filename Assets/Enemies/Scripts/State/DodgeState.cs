using UnityEngine;

public abstract class DodgeState : State
{
    private bool _flipAfterDodge;
    private bool _dodgeOppositeDirection;

    protected D_DodgeState StateData;

    protected bool ShouldPerformCloseRangeAction;
    protected bool ShouldPerformLongRangeAction;
    protected bool IsPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool IsDodgeTimeOver;

    protected bool IsGrounded;

    public float LastDodgeTime;

    public DodgeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DodgeState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
        _dodgeOppositeDirection = false;
    }

    public override void Enter()
    {
        base.Enter();

        IsDodgeTimeOver = false;

        if (_dodgeOppositeDirection)
        {
            Entity.SetVelocity(StateData.dodgeSpeed, StateData.dodgeAngle, Entity.FacingDirection);
        }
        else
        {
            Entity.SetVelocity(StateData.dodgeSpeed, StateData.dodgeAngle, -Entity.FacingDirection);
        }

        if (_flipAfterDodge)
        {
            Entity.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();

        LastDodgeTime = Time.time;
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

    public void ShouldFlipAfterDodge(bool flipAfterIdle) => _flipAfterDodge = flipAfterIdle;

    public void ShouldDodgeInOppositeDirection(bool dodgeOppositeDirection) => _dodgeOppositeDirection = dodgeOppositeDirection;
}
