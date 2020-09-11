﻿public class Boar_PlayerJumpedOverState : PlayerJumpedOverState
{
    private Boar boar;

    public Boar_PlayerJumpedOverState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_PlayerJumpedOverState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
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
}
