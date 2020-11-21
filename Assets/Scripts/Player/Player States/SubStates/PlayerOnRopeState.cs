using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState : PlayerAbilityState
{
    private int _xInput;
    private bool _ropeInputStop;
    private bool _ropeAttached;
    private int _clickCount;

    protected Vector2 PlayerPosition;

    public PlayerOnRopeState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            _ropeInputStop = Player.InputHandler.SecondaryAttackInputStop;

            if (!_ropeAttached && !_ropeInputStop)
            {
                Player.FiniteStateMachine.ChangeState(Player.OnRopeStateAim);
            }
            //else if (!_ropeAttached && _clickCount == 1)
            //{
            //    Player.InputHandler.UseSecondaryAttackInputStop();

            //    AttachRope();
            //}
            //else if (_ropeAttached && _clickCount == 1)
            //{
            //    Player.CheckIfShouldFlip(_xInput);

            //    UpdateRopePositions();
            //}
            //else if (_ropeAttached && _clickCount == 2)
            //{
            //    Player.InputHandler.UseSecondaryAttackInputStop();

            //    ResetRope();

            //    IsAbilityDone = true;
            //}
        }
    }
}
