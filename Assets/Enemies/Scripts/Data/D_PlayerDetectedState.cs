using UnityEngine;

[CreateAssetMenu(fileName = "_PlayerDetectedStateData", menuName = "Data/Enemy State Data/Player Detected State")]
public class D_PlayerDetectedState : ScriptableObject
{
    public float timeForCloseRangeAction = 0.5f;
    public float timeForLongRangeAction = 1.0f;
}
