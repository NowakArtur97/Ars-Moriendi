public class PlayerOnRopeState_Finish : PlayerOnRopeState
{
    public PlayerOnRopeState_Finish(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerOnRopeState onRopeStateData)
        : base(player, playerFiniteStateMachine, animationBoolName, onRopeStateData)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.InputHandler.UseSecondaryAttackInput();

        IsAbilityDone = true;

        ResetRope();
    }

    public void ResetRope()
    {
        Player.RopeJoint.enabled = false;
        Player.RopeHingeAnchorSpriteRenderer.enabled = false;

        Player.MyRopeLineRenderer.positionCount = 2;
        Player.MyRopeLineRenderer.SetPosition(0, PlayerPosition);
        Player.MyRopeLineRenderer.SetPosition(1, PlayerPosition);

        WrapPointsLookup.Clear();
        RopePositions.Clear();
    }
}
