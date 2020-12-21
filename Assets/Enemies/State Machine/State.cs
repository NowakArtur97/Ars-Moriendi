using UnityEngine;

public abstract class State
{
    protected FiniteStateMachine FiniteStateMachine;
    protected Entity Entity;
    protected string AnimationBoolName;

    public float StartTime { get; private set; }

    public State(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName)
    {
        FiniteStateMachine = finiteStateMachine;
        Entity = entity;
        AnimationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        StartTime = Time.time;

        Entity.MyAnimator.SetBool(AnimationBoolName, true);

        DoChecks();
    }

    public virtual void Exit()
    {
        Entity.MyAnimator.SetBool(AnimationBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
}
