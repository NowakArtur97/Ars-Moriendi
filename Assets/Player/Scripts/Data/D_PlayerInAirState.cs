﻿using UnityEngine;

[CreateAssetMenu(fileName = "_InAirStateData", menuName = "Data/Player State Data/In Air State")]
public class D_PlayerInAirState : ScriptableObject
{
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float movementVelocity = 10;
}
