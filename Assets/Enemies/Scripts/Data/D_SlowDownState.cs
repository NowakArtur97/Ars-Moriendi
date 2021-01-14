using UnityEngine;

[CreateAssetMenu(fileName = "_SlowDownStateData", menuName = "Data/Enemy State Data/Slowing Down State")]
public class D_SlowDownState : ScriptableObject
{
    public float decelerationSpeed = 0.1f;
    public float minSlideTime = 0.75f;
}
