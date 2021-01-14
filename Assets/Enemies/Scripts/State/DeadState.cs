using UnityEngine;

public abstract class DeadState : EnemyState
{
    protected D_DeadState StateData;

    public bool IsDead { get; private set; }

    protected float AnimationFinishedTime;

    public DeadState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DeadState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsDead = true;

        Enemy.SetVelocity(StateData.deathVelocity);

        foreach (GameObject effect in StateData.deathEffects)
        {
            GameObject.Instantiate(effect, Enemy.AliveGameObject.transform.position, effect.transform.rotation);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (Time.time >= StartTime + StateData.timeBeforeDeactivation)
            {
                Enemy.gameObject.SetActive(false);
            }
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAnimationFinished = true;

        AnimationFinishedTime = Time.time;
    }
}
