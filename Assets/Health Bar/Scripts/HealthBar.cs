using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public void SetHealth(float health)
    {
        _slider.value = health;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }
}
