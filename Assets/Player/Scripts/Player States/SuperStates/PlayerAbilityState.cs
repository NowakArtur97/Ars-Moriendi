using UnityEngine;

public abstract class PlayerAbilityState : PlayerState
{
    public bool IsAbilityDone { get; protected set; }

    protected bool IsTouchingCeiling;
    private bool _isGrounded;

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

            if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                FiniteStateMachine.ChangeCurrentState(Player.IdleState);
            }
            if (IsTouchingCeiling)
            {
                FiniteStateMachine.ChangeCurrentState(Player.CrouchIdleState);
            }
            else
            {
                FiniteStateMachine.ChangeCurrentState(Player.InAirState);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
        IsTouchingCeiling = Player.CheckIfTouchingCeiling();
    }
}
