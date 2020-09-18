using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    public AttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition) :
        base(finiteStateMachine, entity, animationBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();
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
}
