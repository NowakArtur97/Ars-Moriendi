using UnityEngine;

public class State
{
    protected FiniteStateMachine FiniteStateMachine;
    protected Entity Entity;
    protected string AnimationBoolName;

    public float startTime { get; private set; }

    public State(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName)
    {
        this.FiniteStateMachine = finiteStateMachine;
        this.Entity = entity;
        this.AnimationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;

        Entity.myAnimator.SetBool(AnimationBoolName, true);

        DoChecks();
    }

    public virtual void Exit()
    {
        Entity.myAnimator.SetBool(AnimationBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
    }
}
