﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Area Attack State Data", menuName = "Data/State Data/Area Attack State")]
public class D_AreaAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 15.0f;
    public float projectileStunDamage = 10.0f;
    public float projectileSpeed = 12.0f;
    public float projectileTravelDistance = 8.0f;
    public float projectileGravityScale = 0.0f;
    public int numberOfProjectiles = 6;
}
