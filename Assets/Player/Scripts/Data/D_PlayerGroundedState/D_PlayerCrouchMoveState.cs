using UnityEngine;

[CreateAssetMenu(fileName = "_CrouchMoveStateData", menuName = "Data/Player State Data/Crouch Move State")]
public class D_PlayerCrouchMoveState : ScriptableObject
{
    public float crouchMovementVelocity = 3;
    public float crouchColliderHeight = 0.8f;
}
