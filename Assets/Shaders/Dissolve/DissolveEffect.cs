using UnityEngine;

public class DissolveEffect
{
    private Material _material;
    private float _dissolve;
    private bool _isActive;
    private string _propertyName;

    public void StartEffect()
    {
        if (_isActive)
        {
            _dissolve -= Time.deltaTime;

            if (_dissolve <= 0)
            {
                _dissolve = 0;
                _isActive = false;
            }

            _material.SetFloat(_propertyName, _dissolve);
        }
    }

    public void SetupEffect(EffectsDetails effectsDetails)
    {
        _material = effectsDetails.material;
        _isActive = effectsDetails.activeOnStart;
        _dissolve = effectsDetails.startValue;
        _propertyName = effectsDetails.propertyName;
    }
}
