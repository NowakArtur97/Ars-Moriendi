using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Data/Enemy Data/Enemy Data")]
public class D_EnemyBase : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float wallBehindCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 1.0f;

    public float minAgroDistance = 3.0f;
    public float maxAgroDistance = 4.0f;

    public float closeRangeActionDistance = 1.0f;
    public float longRangeActionDistance = 4.0f;

    public float maxPlayerJumpedOverDistance = 1.0f;

    public float damageHopSpeed = 9.0f;
    public GameObject[] hitPartciles;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
