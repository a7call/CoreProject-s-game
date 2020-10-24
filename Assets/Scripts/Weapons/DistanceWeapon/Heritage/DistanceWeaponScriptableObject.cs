﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Distance Weapon", menuName = "DistanceWeapon")]

public class DistanceWeaponScriptableObject : ScriptableObject
{
    public int damage;
    public LayerMask enemyLayer;
    public GameObject projectile;
    public float AttackDelay;
}