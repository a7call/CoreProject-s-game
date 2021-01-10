using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new  Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public float damage;
    public LayerMask enemyLayer;
    public float AttackDelay;
    public Sprite image;
}
