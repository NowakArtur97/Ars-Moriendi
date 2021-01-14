using UnityEngine;

[CreateAssetMenu(fileName = "_StatsData", menuName = "Data/Player Data/Stats Data")]
public class D_PlayerStats : ScriptableObject
{
    public float maxHealth = 100f;
    public int maxStunResistance = 20;
}
