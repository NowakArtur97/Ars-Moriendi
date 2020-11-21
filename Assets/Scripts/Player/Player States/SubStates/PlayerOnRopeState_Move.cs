using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRopeState_Move : PlayerOnRopeState
{
    public PlayerOnRopeState_Move(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName) : base(player, playerFiniteStateMachine, playerData, animationBoolName)
    {
    }
}
