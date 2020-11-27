using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState : PlayerAbilityState
{
    private bool _isGrounded;

    protected bool RopeInputStop;
    protected Vector2 PlayerPosition;

    protected static bool IsAiming;
    protected static bool RopeAttached;
    protected static bool IsHoldingRope;
    protected static Vector2 AimDirection;
    protected static List<Vector2> RopePositions = new List<Vector2>();
    protected static Dictionary<Vector2, int> WrapPointsLookup = new Dictionary<Vector2, int>();

    public PlayerOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            Player.MyAnmator.SetBool(AnimationBoolName, false);
            Player.MyAnmator.SetBool("idle", true);
        }
        else
        {
            Player.MyAnmator.SetBool(AnimationBoolName, false);
            Player.MyAnmator.SetBool("inAir", true);
        }

        PlayerPosition = Player.transform.position;
        RopeInputStop = Player.InputHandler.SecondaryAttackInputStop;

        if (!IsExitingState)
        {
            if (!RopeAttached && !IsAiming && IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateAttach);
            }
            else if (RopeAttached && !IsAiming && IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateMove);
            }
            else if (!RopeAttached && !IsAiming && !IsHoldingRope)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateFinish);
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Player.CheckIfGrounded();
    }
}
