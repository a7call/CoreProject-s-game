using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Cac Weapon", menuName = "CacWeapon")]
public class CaCWeaponScriptableObject : ScriptableObject
{
    public int damage;
    public LayerMask enemyLayer;
    public float attackRadius;
    public float AttackDelay;
    public float knockBackForce;
    public float knockBackTime;
}
