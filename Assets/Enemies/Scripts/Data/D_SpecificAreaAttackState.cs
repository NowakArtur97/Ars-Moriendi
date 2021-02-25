using UnityEngine;

[CreateAssetMenu(fileName = "_SpecificAreaAttackStateData", menuName = "Data/Enemy State Data/Specific Area Attack State")]
public class D_SpecificAreaAttackState : ScriptableObject
{
    public GameObject attack;
    public float attackDamage = 10f;
    public float attackStunDamage = 7f;
    public float attackSpeed = 0f;
    public Vector2 attackOffset;
    public float timeToDisappear = 2f;
    public bool shouldSpawnInPlayerPosition = true;
}
