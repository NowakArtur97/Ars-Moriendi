using UnityEngine;

public class SpecificAreaAttackState : AttackState
{
    protected D_SpecificAreaAttackState StateData;

    public SpecificAreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition,
        D_SpecificAreaAttackState stateData) : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        GameObject.Instantiate(StateData.attack, AttackPosition.transform.position, Quaternion.identity);
    }
}
