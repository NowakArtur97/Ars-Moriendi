using UnityEngine;

[CreateAssetMenu(fileName = "New Player State Data", menuName = "Data/State Data/Player State")]
public class D_PlayerData : ScriptableObject
{
    [Header("Crouch Move State")]
    public float crouchMovementVelocity = 6;

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
