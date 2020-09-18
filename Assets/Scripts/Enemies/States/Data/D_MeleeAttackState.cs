using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Attack State Data", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;

    public float attackDamage = 10f;

    public LayerMask whatIsPlayer;
}
