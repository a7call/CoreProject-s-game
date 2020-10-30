﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProj : PlayerProjectiles
{
    [SerializeField] float activeTime;
    [SerializeField] protected LayerMask HitLayer;
    
    

    void Update()
    {
        StartCoroutine(destroy());


        dir = (weaponAttackP.attackPoint.position - playerTransform.position).normalized;

        StartCoroutine(destroy());
            RaycastHit2D hit = Physics2D.Raycast(weaponAttackP.attackPoint.position, dir, Mathf.Infinity, HitLayer);

            Debug.DrawRay(weaponAttackP.attackPoint.position, dir * 10, Color.red);
            if (hit.collider != null)
            {
                print("test");
            //Damage
            }
        
    }

    protected IEnumerator destroy()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }

}