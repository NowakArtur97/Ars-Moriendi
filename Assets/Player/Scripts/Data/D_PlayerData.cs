using UnityEngine;

[CreateAssetMenu(fileName = "New Player State Data", menuName = "Data/State Data/Player State")]
public class D_PlayerData : ScriptableObject
{
    [Header("Crouch Move State")]
    public float crouchMovementVelocity = 6;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 ledgeClimbStartOffset = new Vector2(0.3f, 0.5f);
    public Vector2 ledgeClimbStopOffset;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1;
    public float holdTimeDashScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30;
    public float dashDrag = 10;
    public float dashEndMultiplier = 0.2f;
    public float distanceBetweenAfterImages = 0.4f;

    [Header("Rope Move State")]
    public float ropeCrosshairOffset = 3f;
    public LayerMask whatCanYouAttachTo;
    public float ropeMaxCastDistance = 20f;
    public float ropeStartingVelocity = 10f;
    public float ropeSwigForce = 6f;
    public float ropeClimbingSpeed = 4f;
    public Vector2 attachedRopeForce = new Vector2(2, 5);

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
}
