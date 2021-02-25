using UnityEngine;

public class SpecificAreaAttackState : AttackState
{
    protected D_SpecificAreaAttackState StateData;

    private Vector2 _attackPosition;

    public SpecificAreaAttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition,
        D_SpecificAreaAttackState stateData) : base(finiteStateMachine, enemy, animationBoolName, attackPosition)
    {
        StateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        _attackPosition.Set(Enemy.transform.position.x + StateData.attackOffsetX * Enemy.FacingDirection, Enemy.transform.position.y + StateData.attackOffsetY);

        GameObject.Instantiate(StateData.attack, _attackPosition, Quaternion.identity);
    }
}
