using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Attack State Data", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10.0f;
    public float projectileSpeed = 12.0f;
    public float projectileTravelDistance = 8.0f;
    public float projectileGravityScale = 0.0f;
}
