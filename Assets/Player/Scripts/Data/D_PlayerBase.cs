using UnityEngine;

[CreateAssetMenu(fileName = "New Player Base State Data", menuName = "Data/Player State Data/Player Base State")]
public class D_PlayerBase : ScriptableObject
{
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public float ceilingCheckRadius = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
}
