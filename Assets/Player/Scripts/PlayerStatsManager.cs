using System;
using UnityEngine;

public class PlayerStatsManager
{
    private float _maxHealth;
    private float _currentHealth;

    private float _maxStunResistance;
    private float _currentStunResistance;

    public float LastDamageTime { get; private set; }

    public bool IsDead { get; private set; }
    public bool IsStunned { get; private set; }
    public bool IsRolling { get; private set; }

    public Action<float> DamageEvent;
    public Action DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData)
    {
        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = _maxHealth;

        _maxStunResistance = playerStatsData.maxStunResistance;
        _currentStunResistance = _maxStunResistance;
    }

    // TODO: Add potion
    public void Heal(float health) => _currentHealth += health;

    public void TakeDamage(AttackDetails attackDetails)
    {
        _currentHealth -= attackDetails.damageAmmount;
        _currentStunResistance -= attackDetails.stunDamageAmount;
        LastDamageTime = Time.time;

        if (_currentHealth <= 0)
        {
            IsDead = true;
        }

        if (_currentStunResistance <= 0)
        {
            IsStunned = true;
        }

        DamageEvent?.Invoke(_currentHealth);
    }

    public void ExitStun() => IsStunned = false;

    public void ResetStunResistance() => _currentStunResistance = _maxStunResistance;

    public bool IsStunResistanceMax() => _currentStunResistance == _maxStunResistance;

    public void SetIsRolling(bool isRolling) => IsRolling = isRolling;
}
