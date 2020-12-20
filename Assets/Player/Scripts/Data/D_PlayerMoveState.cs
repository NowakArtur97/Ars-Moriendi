using UnityEngine;

[CreateAssetMenu(fileName = "New Player Move State Data", menuName = "Data/Player State Data/Move State")]
public class D_PlayerMoveState : ScriptableObject
{
    public float movementVelocity = 10;
    public float standColliderHeight = 1.466799f;
}
