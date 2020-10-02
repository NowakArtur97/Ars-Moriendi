public class PlayerAbilityState : PlayerState
{
    protected bool IsAbilityDone;

    private bool _isGrounded;

    public PlayerAbilityState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName) : base(player, PlayerFiniteStateMachine, PlayerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAbilityDone)
        {
            if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                FiniteStateMachine.ChangeState(Player.IdleState);
            }
            else
            {
                FiniteStateMachine.ChangeState(Player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
    }
}
