using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Distance", menuName = "DistanceEnemy")]
public class Type2ScriptableObject : EnemyScriptableObject
{
    public float restTime;
    public GameObject projetile;
    public float attackRange;
}
