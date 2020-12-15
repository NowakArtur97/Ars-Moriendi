using UnityEngine;

[CreateAssetMenu(fileName = "New Player Dash State Data", menuName = "Data/Player State Data/Dash State")]
public class D_PlayerDashState : ScriptableObject
{
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1;
    public float holdTimeDashScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30;
    public float dashDrag = 10;
    public float dashEndMultiplier = 0.2f;
    public float distanceBetweenAfterImages = 0.4f;
}
