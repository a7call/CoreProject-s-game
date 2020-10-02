﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce ScriptableObject Type2ScriptableObject hérite en plus des paramètres du ScriptableObject EnemyScriptableObject
/// </summary>


[CreateAssetMenu(fileName = "new Distance", menuName = "DistanceEnemy")]
public class Type2ScriptableObject : EnemyScriptableObject
{
    public float restTime;
    public GameObject projetile;
    public float attackRange;
}
