using UnityEngine;

[CreateAssetMenu(fileName = "New Player State Data", menuName = "Data/State Data/Player State")]
public class D_PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10;

    [Header("Crouch Move State")]
    public float crouchMovementVelocity = 6;

    [Header("Jump State")]
    public float jumpVelocity = 15;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3;

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

    [Header("Player Primary Attack State")]
    public float attackRadius = 0.8f;
    public float attackDamage = 5f;
    public float stunDamageAmount = 1f;
    public float attackMovementSpeed = 0.0f;
    public float attackCooldown = 0.3f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
}
