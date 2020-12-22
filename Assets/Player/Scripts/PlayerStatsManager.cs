using System;

public class PlayerStatsManager
{
    private float _maxHealth;
    private float _currentHealth;

    private float _maxStunResistance;
    private float _currentStunResistance;

    public bool IsDead;
    public bool IsStunned;

    public Action DeathAction;
    public Action DamageAction;

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

            DeathAction?.Invoke();
        }

        if (_currentStunResistance <= 0)
        {
            IsStunned = true;
        }

        DamageAction?.Invoke();
    }

    public void ResetStunResistance() => _currentStunResistance = _maxStunResistance;
}
