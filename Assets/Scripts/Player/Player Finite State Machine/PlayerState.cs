using UnityEngine;

public class PlayerState
{
    public D_PlayerData PlayerData { get; private set; }
    protected Player Player;
    protected PlayerFiniteStateMachine PlayerFiniteStateMachine;

    protected float startTime;

    private string _animationBoolName;

    public PlayerState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData PlayerData, string animationBoolName)
    {
        this.Player = player;
        this.PlayerFiniteStateMachine = PlayerFiniteStateMachine;
        this.PlayerData = PlayerData;
        this._animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        Player.MyAnmator.SetBool(_animationBoolName, true);

        startTime = Time.time;

        Debug.Log(_animationBoolName);
    }

    public virtual void Exit()
    {
        Player.MyAnmator.SetBool(_animationBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
}
