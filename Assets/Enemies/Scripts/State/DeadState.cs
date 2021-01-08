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

        foreach (GameObject effect in StateData.damageEffects)
        {
            GameObject.Instantiate(effect, Enemy.AliveGameObject.transform.position, effect.transform.rotation);
        }

        Enemy.gameObject.SetActive(false);
    }
}
