using UnityEngine;

[CreateAssetMenu(fileName = "New Player Roll State Data", menuName = "Data/Player State Data/Roll State")]
public class D_PlayerRollState : ScriptableObject
{
    public float rollVelocity = 7.5f;
    public float rollColliderHeight = 0.8f;
}
