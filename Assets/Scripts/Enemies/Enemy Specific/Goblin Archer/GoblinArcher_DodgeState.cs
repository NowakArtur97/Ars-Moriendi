﻿public class GoblinArcher_DodgeState : DodgeState
{
    private GoblinArcher goblinArcher;

    public GoblinArcher_DodgeState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DodgeState stateData,
        GoblinArcher goblinArcher) : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this.goblinArcher = goblinArcher;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
