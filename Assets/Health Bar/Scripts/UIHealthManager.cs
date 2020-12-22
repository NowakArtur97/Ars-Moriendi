using UnityEngine;

public class UIHealthManager : MonoBehaviour
{
    [SerializeField]
    private HealthBar _healthBar;

    void Start()
    {
        FindObjectOfType<Player>().StatsManager.DamageEvent += OnDamage;
    }

    public void OnDamage(float currentHealth)
    {
        _healthBar.SetHealth(currentHealth);
    }
}
