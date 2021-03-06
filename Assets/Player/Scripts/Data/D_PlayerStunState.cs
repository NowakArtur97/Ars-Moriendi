﻿using UnityEngine;

[CreateAssetMenu(fileName = "_StunStateData", menuName = "Data/Player State Data/Stun State")]
public class D_PlayerStunState : ScriptableObject
{
    public float stunRecorveryTime = 1.0f;
    public float stunResetTime = 5.0f;
}
