using UnityEngine;

public class Ogre_DeadState : DeadState
{
    private Ogre _ogre;

    private EffectsDetails _effectsDetails;

    public Ogre_DeadState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_DeadState stateData, Ogre ogre)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        _ogre = ogre;
    }

    public override void Enter()
    {
        base.Enter();

        _effectsDetails.material = _ogre.MyMaterial;
        _effectsDetails.activeOnStart = _ogre.DissolveEffectData.activeOnStart;
        _effectsDetails.startValue = _ogre.DissolveEffectData.startValue;
        _effectsDetails.propertyName = _ogre.DissolveEffectData.propertyName;

        _ogre.DissolveEffect.SetupEffect(_effectsDetails);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsAnimationFinished && Time.time >= AnimationFinishedTime + _ogre.DissolveEffectData.timeBeforeDissolving)
        {
            _ogre.DissolveEffect.StartEffect();
        }
    }
}
