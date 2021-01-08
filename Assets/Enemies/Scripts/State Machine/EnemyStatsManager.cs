using UnityEngine;

public class EnemyStatsManager
{
    private float _maxHealth;
    private float _currentHealth;

    private float _maxStunResistance;
    private float _currentStunResistance;
    public float StunRecorveryTime { get; private set; }

    public float LastDamageTime { get; private set; }

    public bool IsDead { get; private set; }
    public bool IsStunned { get; private set; }

    public EnemyStatsManager(D_EnemyStats enemyStatsData)
    {
        _maxHealth = enemyStatsData.maxHealth;
        _currentHealth = _maxHealth;

        _maxStunResistance = enemyStatsData.maxStunResistance;
        _currentStunResistance = _maxStunResistance;
        StunRecorveryTime = enemyStatsData.stunRecorveryTime;
    }

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
    }

    public void ExitStun() => IsStunned = false;

    public void ResetStunResistance() => _currentStunResistance = _maxStunResistance;

    public bool HasMaxStunResistance() => _currentStunResistance == _maxStunResistance;
}
