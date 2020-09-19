using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Attack State Data", menuName = "Data/State Data/Melee Attack State")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3.0f;

    public float stunKnockbackTime = 0.2f;
    public float stunKnockbackSpeed = 20.0f;
    public Vector2 stunKnockbackAngle;
}
