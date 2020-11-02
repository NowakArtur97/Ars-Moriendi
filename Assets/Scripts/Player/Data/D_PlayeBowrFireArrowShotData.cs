using UnityEngine;

[CreateAssetMenu(fileName = "New Player Bow Attack State Data", menuName = "Data/State Data/Player Bow Attack State")]
public class D_PlayerBowFireArrowShotData : ScriptableObject
{
    public float bowShotCooldown = 0.75f;
    public float bowShotMaxHoldTime = 4;
    public float holdTimeBowShotScale = 0.25f;
    public float bowShotVelocity = 30;

    public GameObject fireArrow;
    public float fireArrowDamage = 10.0f;
    public float fireArrowSpeed = 12.0f;
    public float fireArrowTravelDistance = 8.0f;
}
