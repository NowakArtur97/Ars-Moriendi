﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats Data", menuName = "Data/Player State Data/Player Stats")]
public class D_PlayerStatsData : ScriptableObject
{
    public float maxHealth = 100f;
    public int stunResistance = 20;
}
