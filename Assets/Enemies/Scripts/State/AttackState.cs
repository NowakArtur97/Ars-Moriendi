using UnityEngine;

public abstract class AttackState : State
{
    protected Transform AttackPosition;

    protected bool IsAnimationFinished;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;

    public AttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition) :
        base(finiteStateMachine, entity, animationBoolName)
    {
        AttackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.AnimationToStateMachine.attackState = this;

        IsAnimationFinished = false;

        Entity.SetVelocity(0.0f);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMinAgroRange = Entity.CheckIfPlayerInMinAgro;
        IsPlayerInMaxAgroRange = Entity.CheckIfPlayerInMaxAgro;
    }

    public virtual void FinishAttack() => IsAnimationFinished = true;

    public virtual void TriggerAttack() { }
}
