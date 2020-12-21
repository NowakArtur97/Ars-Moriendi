using UnityEngine;

public abstract class DeadState : State
{
    protected D_DeadState StateData;

    public DeadState(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName, D_DeadState stateData)
        : base(finiteStateMachine, entity, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(StateData.deathChunkEffectGO, Entity.AliveGameObject.transform.position, StateData.deathChunkEffectGO.transform.rotation);
        GameObject.Instantiate(StateData.bloodEffectGO, Entity.AliveGameObject.transform.position, StateData.bloodEffectGO.transform.rotation);

        Entity.gameObject.SetActive(false);
    }
}
