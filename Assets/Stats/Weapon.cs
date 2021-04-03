using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "WeaponItem")]
public class Weapon : Item
{
    public float delayBetweenAttack;
    public float damage;
}
