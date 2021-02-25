using UnityEngine;

public class SpecificAreaAttackState : AttackState
{
    protected D_SpecificAreaAttackState StateData;

    protected GameObject SpecificAreaAttack;
    protected SpecificAreaAttack SpecificAreaAttackScript;

    private AttackDetails _attackDetails;

    public SpecificAreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition,
        D_SpecificAreaAttackState stateData) : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        SpecificAreaAttack = GameObject.Instantiate(StateData.attack, AttackPosition.transform.position, Quaternion.identity);
        SpecificAreaAttackScript = SpecificAreaAttack.GetComponent<SpecificAreaAttack>();

        _attackDetails.damageAmmount = StateData.attackDamage;
        _attackDetails.stunDamageAmount = StateData.attackStunDamage;

        SpecificAreaAttackScript.SpawnAttack(StateData.attackSpeed, _attackDetails, StateData.timeToDisappear);
    }
}
