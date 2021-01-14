using UnityEngine;

[CreateAssetMenu(fileName = "_LookForPlayerStateData", menuName = "Data/Enemy State Data/Look For Player State")]
public class D_LookForPlayerState : ScriptableObject
{
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f;
}
