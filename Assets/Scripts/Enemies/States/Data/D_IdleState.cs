using UnityEngine;

[CreateAssetMenu(fileName = "New Idle State Data", menuName = "Data/State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float minimumIdleTime = 1f;
    public float maximumIdleTime = 2f;
}
