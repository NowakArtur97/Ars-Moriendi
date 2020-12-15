using UnityEngine;

[CreateAssetMenu(fileName = "New Slowing Down State Data", menuName = "Data/State Data/Slowing Down State")]
public class D_SlowDownState : ScriptableObject
{
    public float decelerationSpeed = 0.1f;
    public float minSlideTime = 0.75f;
}
