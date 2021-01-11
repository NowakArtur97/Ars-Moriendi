using UnityEngine;

public abstract class EnemyState
{
    protected FiniteStateMachine FiniteStateMachine;
    protected Enemy Enemy;

    protected string AnimationBoolName;
    protected bool IsAnimationFinished;

    public float StartTime { get; private set; }

    public EnemyState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName)
    {
        FiniteStateMachine = finiteStateMachine;
        Enemy = enemy;
        AnimationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log(AnimationBoolName);

        StartTime = Time.time;

        IsAnimationFinished = false;

        Enemy.MyAnimator.SetBool(AnimationBoolName, true);

        if (Enemy.AnimationToStateMachine != null)
        {
            Enemy.AnimationToStateMachine.state = this;
        }

        DoChecks();
    }

    public virtual void Exit()
    {
        Enemy.MyAnimator.SetBool(AnimationBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        if (!Enemy.StatsManager.HasMaxStunResistance() && Time.time >= Enemy.StatsManager.LastDamageTime + Enemy.StatsManager.StunRecorveryTime)
        {
            Enemy.StatsManager.ResetStunResistance();
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() { }
}
