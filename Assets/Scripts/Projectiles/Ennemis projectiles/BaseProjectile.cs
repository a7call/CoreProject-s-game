using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class BaseProjectile : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GetDirection();
    }


}
