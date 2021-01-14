using UnityEngine;

[CreateAssetMenu(fileName = "_BaseStateData", menuName = "Data/Player Data/Base Data")]
public class D_PlayerBase : ScriptableObject
{
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public float ceilingCheckRadius = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
}
