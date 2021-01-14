using UnityEngine;

[CreateAssetMenu(fileName = "_RangedAttackStateData", menuName = "Data/Enemy State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 15.0f;
    public float projectileStunDamage = 10.0f;
    public float projectileSpeed = 12.0f;
    public float projectileTravelDistance = 8.0f;
    public float projectileGravityScale = 0.0f;
}
