﻿using UnityEngine;

public class PlayerSwordAttackState_01 : PlayerSwordAttackState
{
    public PlayerSwordAttackState_01(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, Transform attackPosition,
        D_PlayerSwordAttackData playerSwordAttackData) : base(player, playerFiniteStateMachine, animationBoolName, attackPosition, playerSwordAttackData)
    {
    }
}