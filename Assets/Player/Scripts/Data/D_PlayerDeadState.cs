using UnityEngine;

[CreateAssetMenu(fileName = "_DeadStateData", menuName = "Data/Player State Data/Dead State")]
public class D_PlayerDeadState : ScriptableObject
{
    public GameObject deathChunkEffectGO;
    public GameObject bloodEffectGO;
}
