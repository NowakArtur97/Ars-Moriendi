﻿using UnityEngine;

[CreateAssetMenu(fileName = "_LedgeClimbStateData", menuName = "Data/Player State Data/Ledge Climb State")]
public class D_PlayerLedgeClimbState : ScriptableObject
{
    public Vector2 ledgeClimbStartOffset = new Vector2(0.3f, 0.5f);
    public Vector2 ledgeClimbStopOffset = new Vector2(0.4f, 0.8f);
    public float cornerTolerance = 0.015f;
    public float standCheckOffset = 0.015f;
    public float standColliderHeight = 1.466799f;
}
