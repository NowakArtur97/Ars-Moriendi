using UnityEngine;

public abstract class PlayerState
{
    private float _changeSkillInput;

    protected Player Player;
    protected PlayerFiniteStateMachine FiniteStateMachine;

    protected float StartTime;

    protected bool IsAnimationFinished;
    protected bool IsExitingState;

    protected string AnimationBoolName { get; private set; }

    public PlayerState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName)
    {
        Player = player;
        FiniteStateMachine = playerFiniteStateMachine;
        AnimationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        Player.AnimationToStateMachine.playerState = this;

        DoChecks();

        Player.MyAnmator.SetBool(AnimationBoolName, true);

        StartTime = Time.time;

        Debug.Log(FiniteStateMachine.CurrentState.ToString() + " " + AnimationBoolName);

        IsAnimationFinished = false;
        IsExitingState = false;
    }

    public virtual void Exit()
    {
        Player.MyAnmator.SetBool(AnimationBoolName, false);
        IsExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        _changeSkillInput = Player.InputHandler.NormalizedChangeSkillInput;

        if (_changeSkillInput != 0)
        {
            if (_changeSkillInput == 1)
            {
                Player.SkillManager.ChangeSkillUp();
            }
            else
            {
                Player.SkillManager.ChangeSkillDown();
            }
        }
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() { }

    public void SetAnimationBoolName(string animationBoolName) => AnimationBoolName = animationBoolName;
}
