using UnityEngine;

public class State
{
    protected FiniteStateMachine finiteStateMachine;
    protected Entity entity;
    protected string animationBoolName;

    protected float startTime;

    public State(FiniteStateMachine finiteStateMachine, Entity entity, string animationBoolName)
    {
        this.finiteStateMachine = finiteStateMachine;
        this.entity = entity;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;

        entity.myAnimator.SetBool(animationBoolName, true);
    }

    public virtual void Exit()
    {
        entity.myAnimator.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdateFunction()
    {
    }

    public virtual void PhysicsUpdateFunction()
    {
    }
}
