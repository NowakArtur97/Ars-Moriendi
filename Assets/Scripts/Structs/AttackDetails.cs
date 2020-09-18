using UnityEngine;

public struct AttackDetails
{
    public Vector2 position;

    public float damageAmmount;
    private float attackDamage;

    public AttackDetails(Vector2 position, float attackDamage) : this()
    {
        this.position = position;
        this.attackDamage = attackDamage;
    }
}
