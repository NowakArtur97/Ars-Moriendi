﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Player Wall Jump State Data", menuName = "Data/Player State Data/Wall Jump State")]
public class D_PlayerWallJumpState : ScriptableObject
{
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
}
