using UnityEngine;

[CreateAssetMenu(fileName = "_SpecificAreaAttackStateData", menuName = "Data/Enemy State Data/Specific Area Attack State")]
public class D_SpecificAreaAttackState : ScriptableObject
{
    public GameObject attack;
    public float attackOffsetX = 2.0f;
    public float attackOffsetY = 2.0f;
}
