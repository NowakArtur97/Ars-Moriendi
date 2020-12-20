using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ledge Climb State Data", menuName = "Data/Player State Data/Ledge Climb State")]
public class D_PlayerLedgeClimbState : ScriptableObject
{
    public Vector2 ledgeClimbStartOffset = new Vector2(0.3f, 0.5f);
    public Vector2 ledgeClimbStopOffset = new Vector2(0.4f, 0.8f);
    public float cornerTolerance = 0.015f;
}
