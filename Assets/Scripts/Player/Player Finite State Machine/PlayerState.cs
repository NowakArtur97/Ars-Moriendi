using UnityEngine;

public class PlayerState
{
    private D_PlayerData playerData;
    protected Player player;
    protected PlayerFiniteStateMachine playerFiniteStateMachine;

    protected float startTime;

    private string animationBoolName;

    public PlayerState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, D_PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.playerFiniteStateMachine = playerFiniteStateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        player.MyAnmator.SetBool(animationBoolName, true);

        startTime = Time.time;
    }

    public virtual void Exit()
    {
        player.MyAnmator.SetBool(animationBoolName, true);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
}
