using UnityEngine;

[CreateAssetMenu(fileName = "New Idle State Data", menuName = "Data/State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float minimumIdleTime = 0.4f;
    public float maximumIdleTime = 1f;
}
