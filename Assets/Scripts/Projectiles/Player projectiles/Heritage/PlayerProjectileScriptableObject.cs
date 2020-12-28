using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Projectile", menuName = "Projectile")]
public class PlayerProjectileScriptableObject : ScriptableObject
{
    public float speed;
    public float knockBackForce;
    public float knockBackTime;
}
