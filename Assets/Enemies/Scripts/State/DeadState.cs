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

        GameObject.Instantiate(stateData.deathChunkEffectGO, Entity.aliveGameObject.transform.position, stateData.deathChunkEffectGO.transform.rotation);
        GameObject.Instantiate(stateData.bloodEffectGO, Entity.aliveGameObject.transform.position, stateData.bloodEffectGO.transform.rotation);

        Entity.gameObject.SetActive(false);
    }
}
