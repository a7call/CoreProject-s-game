using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public int damage;
    public LayerMask enemyLayer;
    public float attackRadius;
    public float AttackDelay;
}
