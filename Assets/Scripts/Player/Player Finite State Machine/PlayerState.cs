using UnityEngine;

public class PlayerState
{
    private D_PlayerData _playerData;
    protected Player Player;
    protected PlayerFiniteStateMachine PlayerFiniteStateMachine;

    protected float startTime;

    private string animationBoolName;

    public PlayerState(Player player, PlayerFiniteStateMachine PlayerFiniteStateMachine, D_PlayerData _playerData, string animationBoolName)
    {
        this.Player = player;
        this.PlayerFiniteStateMachine = PlayerFiniteStateMachine;
        this._playerData = _playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        Player.MyAnmator.SetBool(animationBoolName, true);

        startTime = Time.time;

        Debug.Log(animationBoolName);
    }

    public virtual void Exit()
    {
        Player.MyAnmator.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
}
