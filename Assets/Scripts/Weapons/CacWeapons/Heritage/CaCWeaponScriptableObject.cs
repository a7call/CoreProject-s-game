using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Cac Weapon", menuName = "CacWeapon")]
public class CaCWeaponScriptableObject : WeaponScriptableObject
{
    public float attackRadius;
    public float knockBackForce;
    public float knockBackTime;
}
