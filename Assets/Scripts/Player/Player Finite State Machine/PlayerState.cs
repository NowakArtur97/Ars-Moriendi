using UnityEngine;

public class PlayerState
{
    public D_PlayerData PlayerData { get; private set; }
    protected Player Player;
    protected PlayerFiniteStateMachine FiniteStateMachine;

    protected float StartTime;

    protected bool IsAnimationFinished;
    protected bool IsExitingState;

    protected string AnimationBoolName;

    public PlayerState(Player Player, PlayerFiniteStateMachine FiniteStateMachine, D_PlayerData PlayerData, string AnimationBoolName)
    {
        this.Player = Player;
        this.FiniteStateMachine = FiniteStateMachine;
        this.PlayerData = PlayerData;
        this.AnimationBoolName = AnimationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        Player.MyAnmator.SetBool(AnimationBoolName, true);

        StartTime = Time.time;

        Debug.Log(AnimationBoolName);

        IsAnimationFinished = false;
        IsExitingState = false;
    }

    public virtual void Exit()
    {
        Player.MyAnmator.SetBool(AnimationBoolName, false);
        IsExitingState = true;
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() { }
}
