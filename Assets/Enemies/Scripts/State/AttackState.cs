using UnityEngine;

public abstract class AttackState : EnemyState
{
    protected Transform AttackPosition;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public AttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName) :
        base(finiteStateMachine, enemy, animationBoolName)
    {
    }

    public AttackState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, Transform attackPosition) :
        base(finiteStateMachine, enemy, animationBoolName)
    {
        AttackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.AttackAnimationToStateMachine.attackState = this;

        Enemy.SetVelocity(0.0f);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMinAgroRange = Enemy.CheckIfPlayerInMinAgro();
        IsPlayerInMaxAgroRange = Enemy.CheckIfPlayerInMaxAgro();
    }

    public virtual void FinishAttack() => IsAnimationFinished = true;

    public virtual void TriggerAttack() { }
}
