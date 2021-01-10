using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CoollingWeapon", menuName = "CoollingWeapon")]
public class CollingWeaponScriptableObject : WeaponScriptableObject
{
    public float coolingTime = 3f;
    public float coolingDelay = 0.5f;
    public int countMax = 6;
    public float knockBackforce = 15f;
    public float knockBackTime = 0.1f;
}
