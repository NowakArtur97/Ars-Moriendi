using UnityEngine;

[CreateAssetMenu(fileName = "New Player Bow Attack State Data", menuName = "Data/State Data/Player Bow Attack State")]
public class D_PlayerBowFireArrowShotData : ScriptableObject
{
    public float bowShotCooldown = 0.5f;
    public float bowShotMaxHoldTime = 1;
    public float holdTimeBowShotScale = 0.25f;
    public float bowShotVelocity = 30;
}
