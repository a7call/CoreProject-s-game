﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce ScriptableObject Type2ScriptableObject hérite en plus des paramètres du ScriptableObject EnemyScriptableObject
/// </summary>


[CreateAssetMenu(fileName = "new Distance", menuName = "DistanceEnemy")]
public class DistanceScriptableObject : EnemyScriptableObject
{
    public GameObject projetile;
    public float projectileSpeed;
    public float dispersion;

}
