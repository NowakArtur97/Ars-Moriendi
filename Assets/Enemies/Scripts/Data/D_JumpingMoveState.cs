﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Jumping Move State Data", menuName = "Data/State Data/Jumping Move State")]
public class D_JumpingMoveState : ScriptableObject
{
    public float jumpingMovementSpeed = 3f;
    public Vector2 jumpingAngle;
}