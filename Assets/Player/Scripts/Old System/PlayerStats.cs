using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;
    private float healthLeft;

    private void Start()
    {
        healthLeft = maxHealth;
    }

    public void DealDamage(float damageReceived)
    {
        healthLeft -= damageReceived;

        if (healthLeft < 0.1f)
        {
            Die();
        }
    }

    private void Die()
    {
        ObjectPoolManager.Instance.GetFromPool(ObjectPoolType.BLOOD_PARTICLE_EFFECT);
        ObjectPoolManager.Instance.GetFromPool(ObjectPoolType.MEAT_CHUNK_EFFECT);

        Destroy(gameObject);
    }
}
