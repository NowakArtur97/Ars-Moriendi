using UnityEngine;

[CreateAssetMenu(fileName = "_IdleStateData", menuName = "Data/Enemy State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float minimumIdleTime = 1f;
    public float maximumIdleTime = 2f;
}
