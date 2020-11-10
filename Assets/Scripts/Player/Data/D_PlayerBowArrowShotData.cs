using UnityEngine;

[CreateAssetMenu(fileName = "New Player Bow Attack State Data", menuName = "Data/State Data/Player Bow Attack State")]
public class D_PlayerBowArrowShotData : ScriptableObject
{
    public float bowShotCooldown = 0.75f;
    public float bowShotMaxHoldTime = 4;
    public float holdTimeAimScale = 0.25f;
    public float minBowShotAngleX = 0.1f;
    public float minBowShotAngleY = -0.7f;
    public float maxBowShotAngleY = 0.7f;

    public GameObject arrow;
    public float arrowDamage = 10.0f;
    public float arrowSpeed = 12.0f;
    public float arrowTravelDistance = 8.0f;
    public float arrowGravityScale = 1.0f;

    public GameObject aimingPoint;
    public int numberOfAimingPoints = 50;
    public float spaceBetweenAimingPoints = 0.025f;
}
