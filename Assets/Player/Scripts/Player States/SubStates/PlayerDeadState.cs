using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private D_PlayerDeadState _deadStateData;

    private float _animationFinishedTime;

    public PlayerDeadState(Player player, PlayerFiniteStateMachine playerFiniteStateMachine, string animationBoolName, D_PlayerDeadState deadStateData)
        : base(player, playerFiniteStateMachine, animationBoolName)
    {
        _deadStateData = deadStateData;
    }

    public override void Enter()
    {
        base.Enter();

        foreach (GameObject effect in _deadStateData.deathEffects)
        {
            GameObject.Instantiate(effect, Player.AliveGameObject.transform.position, effect.transform.rotation);
        }

        Player.SetVelocityX(_deadStateData.deathVelocity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            if (Time.time >= _animationFinishedTime + _deadStateData.timeBeforeRestartingLevel)
            {
                Player.StatsManager.DeathEvent?.Invoke();
            }
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        _animationFinishedTime = Time.time;

        Player.MySpriteRenderer.enabled = false;
        Player.MyBoxCollider2D.enabled = false;
        Player.MyRigidbody.isKinematic = true;
    }
}
