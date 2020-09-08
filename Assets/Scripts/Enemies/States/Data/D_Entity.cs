using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public LayerMask whatIsGround;
}
