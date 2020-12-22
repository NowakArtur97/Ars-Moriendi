using System;

public class PlayerStatsManager
{
    private float _maxHealth;
    private float _currentHealth;

    private float _maxStunResistance;
    private float _currentStunResistance;

    public bool IsDead;
    public bool IsStunned;

    // TODO: use to restart level/load checkpoint
    public Action DeathEvent;
    public Action<float> DamageEvent;

    public PlayerStatsManager(D_PlayerStatsData playerStatsData)
    {
        _maxHealth = playerStatsData.maxHealth;
        _currentHealth = playerStatsData.maxHealth;

        _maxStunResistance = playerStatsData.maxStunResistance;
        _currentStunResistance = playerStatsData.maxStunResistance;
    }

    public void Heal(float health) => _currentHealth += health;

    public void TakeDamage(float damageAmmount, float stunDamageAmount)
    {
        _currentHealth -= damageAmmount;
        _currentStunResistance -= stunDamageAmount;

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

    public void ResetStunResistance() => _currentStunResistance = _maxStunResistance;
}
