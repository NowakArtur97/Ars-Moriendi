using UnityEngine;

[CreateAssetMenu(fileName = "_StatsData", menuName = "Data/Enemy Data/Stats Data")]
public class D_EnemyStats : ScriptableObject
{
    public float maxHealth = 20f;

    public int maxStunResistance = 10;
    public float stunRecorveryTime = 1.0f;
}
