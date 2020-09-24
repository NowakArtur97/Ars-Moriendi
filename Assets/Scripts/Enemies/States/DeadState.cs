using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;

    public DeadState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DeadState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGameObject.transform.position, stateData.deathChunkParticle.transform.rotation);
        GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGameObject.transform.position, stateData.deathBloodParticle.transform.rotation);

        entity.gameObject.SetActive(false);
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
