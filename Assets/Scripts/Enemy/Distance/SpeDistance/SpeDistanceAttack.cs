using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeDistanceAttack : Type2Attack
{
    [SerializeField] protected GameObject EggsProjectiles;
    private bool isSpeRdy = false;
    private bool isSpeReloaded = true;
    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        StartCoroutine("CanShoot");
        StartCoroutine("CheckShootSpe");
    }

    protected override IEnumerator CanShoot()
    {
        if (type2Mouvement.isShooting && isReadytoShoot && !isSpeRdy)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }else if (isSpeRdy && type2Mouvement.isShooting && isReadytoShoot)
        {
            
            isSpeRdy = false;
            isReadytoShoot = false;
            Eggs();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }

    protected override void ResetAggro()
    {
        base.ResetAggro();
    }

    protected override void SetData()
    {
        base.SetData();
    }

    protected override void Shoot()
    {
        base.Shoot();
    }

    protected void Eggs()
    {
        GameObject.Instantiate(EggsProjectiles, transform.position, Quaternion.identity);
    }

    protected IEnumerator CheckShootSpe()
    {
        if (!isSpeRdy && isSpeReloaded)
        {
            isSpeRdy = true;
            isSpeReloaded = false;
            yield return new WaitForSeconds(10f);
            isSpeReloaded = true;
        }
        else
        {
            yield return null;
        }
        

    }

  
}
