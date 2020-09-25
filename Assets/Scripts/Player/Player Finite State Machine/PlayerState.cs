using UnityEngine;

public class PlayerState
{
    private D_PlayerData playerData;
    protected Player player;
    protected PlayerStateMachine playerStateMachine;

    protected float startTime;

    private string animationBoolName;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, D_PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        startTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
}
