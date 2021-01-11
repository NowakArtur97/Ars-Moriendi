using UnityEngine;

public class EnemyUIHealthManager : MonoBehaviour
{
    [SerializeField]
    private HealthBar _healthBar;

    private EnemyStatsManager _statsManager;

    void Start()
    {
        _statsManager = GetComponentInParent<Enemy>().StatsManager;
        _statsManager.DamageEvent += OnDamage;
    }

    public void OnDamage(float currentHealth)
    {
        _healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            _statsManager.DamageEvent -= OnDamage;
        }
    }
}
