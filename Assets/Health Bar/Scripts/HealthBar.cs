using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Image _fill;
    [SerializeField]
    private Gradient _healthGradient;

    public void SetMaxHealth(float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;

        _fill.color = _healthGradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        _slider.value = health;

        _fill.color = _healthGradient.Evaluate(_slider.normalizedValue);

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
