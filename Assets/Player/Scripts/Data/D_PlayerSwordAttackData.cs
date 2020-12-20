using UnityEngine;

[CreateAssetMenu(fileName = "New Player Sword Attack State Data", menuName = "Data/Player State Data/Sword Attack State")]
public class D_PlayerSwordAttackData : ScriptableObject
{
    public float attackRadius = 0.8f;
    public float attackDamage = 5f;
    public float stunDamageAmount = 1f;
    public float attackCooldown = 0.3f;
    public float attackVelocity = 5;
    public int comboAttackIndex = 1;
    public LayerMask whatIsEnemy;
}
