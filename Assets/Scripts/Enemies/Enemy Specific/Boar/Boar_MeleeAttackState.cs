using UnityEngine;

public class Boar_MeleeAttackState : MeleeAttackState
{
    private Boar boar;

    public Boar_MeleeAttackState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, Transform attackPosition,
        D_MeleeAttackState stateData, Boar boar) : base(finiteStateMachine, entity, animationBoolName, attackPosition, stateData)
    {
        this.boar = boar;
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
