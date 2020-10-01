using UnityEngine;

[CreateAssetMenu(fileName = "New Player State Data", menuName = "Data/State Data/Player State")]
public class D_PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10.0f;

    [Header("Jump State")]
    public float jumpVelocity = 15.0f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
