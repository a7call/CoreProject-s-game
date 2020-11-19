﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>


public class AngleProjectile : Projectile
{
   public float angleDecalage;
    
    Vector3 directionTir;


    // private float dist;
    //private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
        ConeShoot();

    }


    protected override void Lauch()
    {
        transform.Translate(directionTir * base.speed * Time.deltaTime);
    }

    protected void ConeShoot()
    {
        directionTir = Quaternion.AngleAxis(angleDecalage, Vector3.forward) * dir;
    }

}
