using UnityEngine;

public abstract class PlayerOnRopeState : PlayerAbilityState
{
    protected D_PlayerOnRopeState OnRopeStateData;

    protected bool IsGrounded;
    protected bool RopeInputStop;
    protected Vector2 PlayerPosition;

    public PlayerOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerOnRopeState onRopeStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        OnRopeStateData = onRopeStateData;
    }

    public override void Enter()
    {
        base.Enter();

        SetAnimationBasedOnPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        SetAnimationBasedOnPosition();

        PlayerPosition = Player.AliveGameObject.transform.position;
        RopeInputStop = Player.InputHandler.SecondaryInputStop;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Player.CheckIfGrounded();
    }

    private void SetAnimationBasedOnPosition()
    {
        if (IsGrounded)
        {
            Player.MyAnmator.SetBool("inAir", false);
            Player.MyAnmator.SetBool("idle", true);
        }
        else
        {
            Player.MyAnmator.SetBool("idle", false);
            Player.MyAnmator.SetBool("inAir", true);
        }
    }
}
