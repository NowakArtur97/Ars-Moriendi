using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinished = false;

    public AttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition) :
        base(finiteStateMachine, entity, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        entity.animationToStateMachine.attackState = this;

        isAnimationFinished = false;

        entity.SetVelocity(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdateFunction()
    {
        base.LogicUpdateFunction();
    }

    public override void PhysicsUpdateFunction()
    {
        base.PhysicsUpdateFunction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
