using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_DeadStateData", menuName = "Data/Enemy State Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    public List<GameObject> deathEffects;
    public float timeBeforeDeactivation = 2.0f;
    public float deathVelocity = 0.0f;
}
