using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_DeadStateData", menuName = "Data/Player State Data/Dead State")]
public class D_PlayerDeadState : ScriptableObject
{
    public List<GameObject> deathEffects;
    public float timeBeforeRestartingLevel = 2.0f;
    public float deathVelocity = 0.0f;
}
