using UnityEngine;

[CreateAssetMenu(fileName = "_BaseData", menuName = "Data/Enemy Data/Base Data")]
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

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
