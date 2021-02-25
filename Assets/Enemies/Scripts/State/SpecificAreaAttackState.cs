using UnityEngine;

public class SpecificAreaAttackState : AttackState
{
    protected D_SpecificAreaAttackState StateData;

    protected GameObject SpecificAreaAttack;
    protected SpecificAreaAttack SpecificAreaAttackScript;

    private AttackDetails _attackDetails;
    private Vector2 _attackPosition;

    public SpecificAreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_SpecificAreaAttackState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        if (StateData.shouldSpawnInPlayerPosition)
        {
            _attackPosition.Set(Enemy.AliveGameObject.transform.position.x + Enemy.GetDistanceToPlayer() * Enemy.FacingDirection,
                Enemy.AliveGameObject.transform.position.y); ;
        }
        else
        {
            _attackPosition = AttackPosition.transform.position;
        }

        _attackPosition.x += StateData.attackOffset.x * Enemy.FacingDirection;
        _attackPosition.y += StateData.attackOffset.y;

        SpecificAreaAttack = GameObject.Instantiate(StateData.attack, _attackPosition, Quaternion.identity));
        SpecificAreaAttackScript = SpecificAreaAttack.GetComponent<SpecificAreaAttack>();

        _attackDetails.damageAmmount = StateData.attackDamage;
        _attackDetails.stunDamageAmount = StateData.attackStunDamage;

        SpecificAreaAttackScript.SpawnAttack(StateData.attackSpeed, _attackDetails, StateData.timeToDisappear);
    }
}
