using UnityEngine;

public class Boar_DeadState : DeadState
{
    private Boar _boar;

    private float _dissolve;
    private bool _isDissolving;

    public Boar_DeadState(FiniteStateMachine finiteStateMachine, Enemy entity, string animationBoolName, D_DeadState stateData, Boar boar)
        : base(finiteStateMachine, entity, animationBoolName, stateData)
    {
        this._boar = boar;
    }

    public override void Enter()
    {
        base.Enter();

        _isDissolving = true;
        // TODO: ENEMY Move to effects data
        _dissolve = 1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: ENEMY Move to effects data
        if (IsAnimationFinished && Time.time >= AnimationFinishedTime + 0.5f)
        {
            if (_isDissolving)
            {
                _dissolve -= Time.deltaTime;

                if (_dissolve <= 0)
                {
                    _dissolve = 0;
                    _isDissolving = false;
                }

                _boar.MyMaterial.SetFloat("_Fade", _dissolve);
            }
        }
    }
}
