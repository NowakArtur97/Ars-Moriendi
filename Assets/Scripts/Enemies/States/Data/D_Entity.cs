using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.3f;
    public float groundCheckDistance = 0.5f;

    public LayerMask whatIsGround;
}
