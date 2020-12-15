using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ledge Climb State Data", menuName = "Data/Player State Data/Ledge Climb State")]
public class D_PlayerLedgeClimbState : MonoBehaviour
{
    public Vector2 ledgeClimbStartOffset = new Vector2(0.3f, 0.5f);
    public Vector2 ledgeClimbStopOffset;
}
