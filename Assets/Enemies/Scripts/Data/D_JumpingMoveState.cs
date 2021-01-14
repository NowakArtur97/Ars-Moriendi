using UnityEngine;

[CreateAssetMenu(fileName = "_JumpingMoveStateData", menuName = "Data/Enemy State Data/Jumping Move State")]
public class D_JumpingMoveState : ScriptableObject
{
    public float jumpingMovementSpeed = 3f;
    public Vector2 jumpingAngle;
}
