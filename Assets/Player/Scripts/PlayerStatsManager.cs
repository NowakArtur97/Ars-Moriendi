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

    public Action<float> DamageEvent;
    // TODO: use to restart level/load checkpoint
    public Action DeathEvent;

    public PlayerStatsManager(D_PlayerStats playerStatsData)
    {
        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = playerStatsData.maxHealth;

        _maxStunResistance = playerStatsData.maxStunResistance;
        _currentStunResistance = playerStatsData.maxStunResistance;
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

            DeathEvent?.Invoke();
        }

        if (_currentStunResistance <= 0)
        {
            IsStunned = true;
        }

        DamageEvent?.Invoke(_currentHealth);
    }

    public void ResetStunResistance()
    {
        IsStunned = false;
        _currentStunResistance = _maxStunResistance;
    }

    public bool IsStunResistanceMax() => _currentStunResistance == _maxStunResistance;
}
