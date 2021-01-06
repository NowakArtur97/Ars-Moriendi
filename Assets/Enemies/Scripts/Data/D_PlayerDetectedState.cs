using UnityEngine;

[CreateAssetMenu(fileName = "New Player Detected State Data", menuName = "Data/State Data/Player Detected State")]
public class D_PlayerDetectedState : ScriptableObject
{
    public float timeForCloseRangeAction = 0.5f;
    public float timeForLongRangeAction = 1.0f;
}
