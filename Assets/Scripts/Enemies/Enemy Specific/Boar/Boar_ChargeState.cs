﻿public class Boar_ChargeState : ChargeState
{
    private Boar boar;

    public Boar_ChargeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_ChargeState stateData, Boar boar)
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

        if (!isDetectingLedge || isDetectingWall)
        {
            // TODO: LOOK FOR PLAYER STATE
            finiteStateMachine.ChangeState(boar.idleState);
        }
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
