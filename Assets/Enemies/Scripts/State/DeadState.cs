using UnityEngine;

public abstract class DeadState : EnemyState
{
    protected D_DeadState StateData;

    public DeadState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DeadState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(StateData.deathChunkEffectGO, Enemy.AliveGameObject.transform.position, StateData.deathChunkEffectGO.transform.rotation);
        GameObject.Instantiate(StateData.bloodEffectGO, Enemy.AliveGameObject.transform.position, StateData.bloodEffectGO.transform.rotation);

        Enemy.gameObject.SetActive(false);
    }
}
