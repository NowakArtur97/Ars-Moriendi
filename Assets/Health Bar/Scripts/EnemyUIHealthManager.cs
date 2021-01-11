using UnityEngine;

public class EnemyUIHealthManager : MonoBehaviour
{
    [SerializeField]
    private HealthBar _healthBar;

    void Start()
    {
        // TODO: Enemy Unsubscribe events
        GetComponentInParent<Enemy>().StatsManager.DamageEvent += OnDamage;
    }

    public void OnDamage(float currentHealth)
    {
        _healthBar.SetHealth(currentHealth);
    }
}
