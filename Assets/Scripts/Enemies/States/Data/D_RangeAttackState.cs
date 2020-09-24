using UnityEngine;

[CreateAssetMenu(fileName = "New Range Attack State Data", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10.0f;
    public float projectileSpeed = 12.0f;
    public float projectileTravelDistance = 8.0f;
}
