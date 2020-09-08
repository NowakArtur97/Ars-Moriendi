using UnityEngine;

public class State
{
    protected FiniteStateMachine finiteStateMachine;
    protected Entity entity;

    protected float startTime;

    public State(FiniteStateMachine finiteStateMachine, Entity entity)
    {
        this.finiteStateMachine = finiteStateMachine;
        this.entity = entity;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdateFunction()
    {
    }

    public virtual void PhysicsUpdateFunction()
    {
    }
}
