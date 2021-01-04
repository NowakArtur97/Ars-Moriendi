using UnityEngine;

public abstract class PlayerAbilityState : PlayerState
{
    public bool IsAbilityDone { get; protected set; }

    protected bool IsTouchingCeiling;
    protected bool IsGrounded;

    public PlayerAbilityState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        IsAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();

        IsAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAbilityDone)
        {
            Debug.Log("Ability is done");

            if (IsTouchingCeiling)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchIdleState);
            }
            else if (IsGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                FiniteStateMachine.ChangeCurrentState(Player.IdleState);
            }
            else if (!IsGrounded)
            {
                FiniteStateMachine.ChangeCurrentState(Player.InAirState);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
        IsTouchingCeiling = Player.CheckIfTouchingCeiling();
    }

    public virtual bool CanUseAbility() { return true; }
}