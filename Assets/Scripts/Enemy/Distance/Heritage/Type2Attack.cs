using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using UnityEngine;

public class Type2Attack : EnemyAttack
{
    [SerializeField] protected Type2ScriptableObject Type2Data;

    private Type2Mouvement type2Mouvement;
    private bool isReadytoShoot = true;
    private float restTime;
    private GameObject projetile;

    // Ref to Type2Mouvement + base 
    protected void Awake()
    {
        type2Mouvement = GetComponent<Type2Mouvement>(); 
    }

    protected virtual IEnumerator CanShoot()
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
    protected virtual void Shoot()
    {
        GameObject.Instantiate(projetile, transform.position, Quaternion.identity);
    }


    // Reset la couroutine CanShoot
    protected virtual void ResetAggro()
    {
        if (!type2Mouvement.isShooting)
        {
            StopCoroutine("CanShoot");
            isReadytoShoot = true;
        }
    }

    protected virtual void SetData()
    {
        restTime = Type2Data.restTime;
        projetile = Type2Data.projetile;
    }
}
