using UnityEngine;

[CreateAssetMenu(fileName = "New Player On Rope State Data", menuName = "Data/Player State Data/On Rope State")]
public class D_PlayerOnRopeState : ScriptableObject
{
    public float ropeCrosshairOffset = 3f;
    public LayerMask whatCanYouAttachTo;
    public float ropeMaxCastDistance = 20f;
    public float ropeStartingVelocity = 10f;
    public float ropeSwigForce = 6f;
    public float ropeClimbingSpeed = 4f;
    public Vector2 attachedRopeForce = new Vector2(2, 5);
}
