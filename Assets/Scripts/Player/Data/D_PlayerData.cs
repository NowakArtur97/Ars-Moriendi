using UnityEngine;

[CreateAssetMenu(fileName = "New Player State Data", menuName = "Data/State Data/Player State")]
public class D_PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10.0f;

    [Header("Jump State")]
    public float jumpVelocity = 15.0f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3.0f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3.0f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
}
