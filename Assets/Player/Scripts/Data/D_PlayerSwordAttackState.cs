using UnityEngine;

[CreateAssetMenu(fileName = "New Player Sword Attack State Data", menuName = "Data/Player State Data/Sword Attack State")]
public class D_PlayerSwordAttackState : ScriptableObject
{
    public float attackRadius = 0.8f;
    public float attackDamage = 5f;
    public float stunDamageAmount = 1f;
    public float attackVelocity = 5;
    public LayerMask whatIsEnemy;
}
