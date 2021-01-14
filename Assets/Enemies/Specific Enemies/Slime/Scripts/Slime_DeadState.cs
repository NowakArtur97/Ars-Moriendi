using UnityEngine;

public class Slime_DeadState : DeadState
{
    private Slime _slime;

    private EffectsDetails _effectsDetails;

    public Slime_DeadState(FiniteStateMachine finiteStateMachine, Enemy enemy, string animationBoolName, D_DeadState stateData, Slime slime)
        : base(finiteStateMachine, enemy, animationBoolName, stateData)
    {
        _slime = slime;
    }

    public override void Enter()
    {
        base.Enter();

        _effectsDetails.material = _slime.MyMaterial;
        _effectsDetails.activeOnStart = _slime.DissolveEffectData.activeOnStart;
        _effectsDetails.startValue = _slime.DissolveEffectData.startValue;
        _effectsDetails.propertyName = _slime.DissolveEffectData.propertyName;

        _slime.DissolveEffect.SetupEffect(_effectsDetails);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished && Time.time >= AnimationFinishedTime + _slime.DissolveEffectData.timeBeforeDissolving)
        {
            _slime.DissolveEffect.StartEffect();
        }
    }
}
