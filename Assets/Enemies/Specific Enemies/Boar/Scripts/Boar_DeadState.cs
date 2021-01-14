using UnityEngine;

public class Boar_DeadState : DeadState
{
    private Boar _boar;

    private EffectsDetails _effectsDetails;

    public Boar_DeadState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_DeadState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _boar = boar;
    }

    public override void Enter()
    {
        base.Enter();

        _effectsDetails.material = _boar.MyMaterial;
        _effectsDetails.activeOnStart = _boar.DissolveEffectData.activeOnStart;
        _effectsDetails.startValue = _boar.DissolveEffectData.startValue;
        _effectsDetails.propertyName = _boar.DissolveEffectData.propertyName;

        _boar.DissolveEffect.SetupEffect(_effectsDetails);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished && Time.time >= AnimationFinishedTime + _boar.DissolveEffectData.timeBeforeDissolving)
        {
            _boar.DissolveEffect.StartEffect();
        }
    }
}
