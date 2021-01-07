using UnityEngine;

public abstract class DodgeState : EnemyState
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

    public DodgeState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DodgeState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
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
            Enemy.SetVelocity(StateData.dodgeSpeed, StateData.dodgeAngle, Enemy.FacingDirection);
        }
        else
        {
            Enemy.SetVelocity(StateData.dodgeSpeed, StateData.dodgeAngle, -Enemy.FacingDirection);
        }

        if (_flipAfterDodge)
        {
            Enemy.Flip();
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

        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        isPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();

        ShouldPerformCloseRangeAction = Enemy.CheckIfPlayerInCloseRangeAction();
        ShouldPerformLongRangeAction = Enemy.CheckIfPlayerInLongRangeAction();

        IsGrounded = Enemy.CheckIfGrounded();
    }

    public void ShouldFlipAfterDodge(bool flipAfterIdle) => _flipAfterDodge = flipAfterIdle;

    public void ShouldDodgeInOppositeDirection(bool dodgeOppositeDirection) => _dodgeOppositeDirection = dodgeOppositeDirection;
}
