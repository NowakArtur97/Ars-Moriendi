using UnityEngine;

[CreateAssetMenu(fileName = "New Player State Data", menuName = "Data/State Data/Player State")]
public class D_PlayerBaseData : ScriptableObject
{
    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
}
