using UnityEngine;

public class PlayerDeadState : PlayerState
{
    private D_PlayerDeadState _deadStateData;

    private EffectsDetails _effectsDetails;

    private float _animationFinishedTime;
    private float _timeToDissolve;

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

        _effectsDetails.material = Player.MyMaterial;
        _effectsDetails.activeOnStart = Player.DissolveEffectData.activeOnStart;
        _effectsDetails.startValue = Player.DissolveEffectData.startValue;
        _effectsDetails.propertyName = Player.DissolveEffectData.propertyName;

        Player.DissolveEffect.SetupEffect(_effectsDetails);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished)
        {
            _timeToDissolve = _animationFinishedTime + Player.DissolveEffectData.timeBeforeDissolving;

            if (Time.time >= _timeToDissolve)
            {
                Player.DissolveEffect.StartEffect();
            }

            if (Time.time >= _timeToDissolve + _deadStateData.timeBeforeRestartingLevel)
            {
                Player.StatsManager.DeathEvent?.Invoke();
            }
        }
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        _animationFinishedTime = Time.time;
    }
}
