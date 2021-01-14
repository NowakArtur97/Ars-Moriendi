using UnityEngine;

[CreateAssetMenu(fileName = "_JumpStateData", menuName = "Data/Player State Data/Jump State")]
public class D_PlayerJumpState : ScriptableObject
{
    public float jumpVelocity = 15;
    public int amountOfJumps = 1;
}
