using UnityEngine;

[CreateAssetMenu(fileName = "_StunStateData", menuName = "Data/Enemy State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3.0f;

    public float stunKnockbackTime = 0.2f;
    public float stunKnockbackSpeed = 20.0f;
    public Vector2 stunKnockbackAngle;
}
