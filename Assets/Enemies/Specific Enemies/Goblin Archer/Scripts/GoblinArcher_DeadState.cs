using UnityEngine;

public class GoblinArcher_DeadState : DeadState
{
    private GoblinArcher _goblinArcher;

    private EffectsDetails _effectsDetails;

    public GoblinArcher_DeadState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_DeadState stateData, GoblinArcher goblinArcher)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _goblinArcher = goblinArcher;
    }

    public override void Enter()
    {
        base.Enter();

        _effectsDetails.material = _goblinArcher.MyMaterial;
        _effectsDetails.activeOnStart = _goblinArcher.DissolveEffectData.activeOnStart;
        _effectsDetails.startValue = _goblinArcher.DissolveEffectData.startValue;
        _effectsDetails.propertyName = _goblinArcher.DissolveEffectData.propertyName;

        _goblinArcher.DissolveEffect.SetupEffect(_effectsDetails);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished && Time.time >= AnimationFinishedTime + _goblinArcher.DissolveEffectData.timeBeforeDissolving)
        {
            _goblinArcher.DissolveEffect.StartEffect();
        }
    }
}

