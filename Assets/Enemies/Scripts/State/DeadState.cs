using UnityEngine;

public abstract class DeadState : EnemyState
{
    protected D_DeadState StateData;

    public bool IsDead { get; private set; }
    protected bool HasStopped;

    protected float AnimationFinishedTime;
    protected float CurrentVelocity;

    public DeadState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DeadState stateData)
        : base(finiteStateMachine, enemy, animationBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsDead = true;

        SetVelocityAfterDeath();

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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!HasStopped && StateData.shouldContinueWithCurrentVelocity)
        {
            SlowDown();
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAnimationFinished = true;

        AnimationFinishedTime = Time.time;
    }

    private void SlowDown()
    {
        CurrentVelocity = Mathf.Abs(CurrentVelocity - StateData.decelerationSpeed);
        Enemy.SetVelocity(CurrentVelocity);

        if (CurrentVelocity <= 0.1f)
        {
            HasStopped = true;
            Enemy.SetVelocity(0.0f);
        }
    }

    private void SetVelocityAfterDeath()
    {
        if (StateData.shouldStopImmediately)
        {
            Enemy.SetVelocity(0.0f);
        }
        else if (StateData.shouldContinueWithCurrentVelocity)
        {
            CurrentVelocity = Enemy.MyRigidbody2D.velocity.x;
        }
        else
        {
            Enemy.SetVelocity(StateData.deathVelocity);
        }
    }
}
