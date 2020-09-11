using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using UnityEngine;

public class Type2Attack : EnemyAttack
{
    private Type2Mouvement type2Mouvement;
    private bool isReadytoShoot = true;
    public float restTime;
    public GameObject projetile;

    // Ref to Type2Mouvement + base 
    protected override void Start()
    {
        base.Start();
        type2Mouvement = GetComponent<Type2Mouvement>();
      
    }

    protected override void Update()
    {

        StartCoroutine("CanShoot");
       // ResetAggro();
    }
    private IEnumerator CanShoot()
    {
        if (type2Mouvement.isShooting && isReadytoShoot)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }
    // Instansiate projectiles
    private void Shoot()
    {
        GameObject.Instantiate(projetile, transform.position, Quaternion.identity);
    }


    // Reset la couroutine CanShoot
    private void ResetAggro()
    {
        if (!type2Mouvement.isShooting)
        {
            StopCoroutine("CanShoot");
            isReadytoShoot = true;
        }
    }
}
