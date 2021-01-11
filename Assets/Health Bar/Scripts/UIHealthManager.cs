using UnityEngine;

public class UIHealthManager : MonoBehaviour
{
    [SerializeField]
    private HealthBar _healthBar;

    private PlayerStatsManager _statsManager;

    void Start()
    {
        _statsManager = FindObjectOfType<Player>().StatsManager;
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
