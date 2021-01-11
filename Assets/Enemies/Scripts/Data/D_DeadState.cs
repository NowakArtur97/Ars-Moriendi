using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dead State Data", menuName = "Data/State Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    public List<GameObject> damageEffects;
    public float timeBeforeDestroyingObject = 2.0f;
    public float deathVelocity = 0.0f;
}
