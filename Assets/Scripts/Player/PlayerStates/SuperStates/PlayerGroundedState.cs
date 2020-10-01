using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;

    private bool JumpInput;

    public PlayerGroundedState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        JumpInput = Player.InputHandler.JumpInput;

        if (JumpInput)
        {
            Player.InputHandler.UseJumpInput();
            PlayerFiniteStateMachine.ChangeState(Player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
