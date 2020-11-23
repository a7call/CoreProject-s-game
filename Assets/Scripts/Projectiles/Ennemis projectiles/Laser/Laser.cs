﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Projectile.cs
/// Contient les fonctions de la classe mères
/// </summary>
public class Laser : Projectile
{
    protected bool damageDone = false;
    private bool ReadyToShoot = false;
    [SerializeField] protected float ShootDelay;
    [SerializeField] public float ActiveTime;
    [SerializeField] protected LayerMask HitLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        GetDirection();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (transform.parent.gameObject == null)
        {
            Destroy(gameObject);
        } 
        StartCoroutine(OkToShoot());

        if (ReadyToShoot == true )
        {
            StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, HitLayer);

            Debug.DrawRay(transform.position, dir * 10, Color.red);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            }
           

        }
    }

    protected IEnumerator destroy()
    {
        yield return new WaitForSeconds(ActiveTime);
        Destroy(gameObject);
    }

    protected IEnumerator OkToShoot()
    {
        yield return new WaitForSeconds(ShootDelay);
        ReadyToShoot = true;
    }
}






