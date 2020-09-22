using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeDistance : Distance
{

    void Start()
    {
        SetFirstPatrolPoint();
        SetData();
        SetMaxHealth();
    }
    private void Update()
    {
        // récupération de l'aggro
        Aggro();
        // script de patrol
        Patrol();
        // suit le path créé et s'arrête pour tirer
        if(!isShooting ) MoveToPath();
        // Couroutine gérant les shoots (à modifier)
        StartCoroutine("CanShoot");
        StartCoroutine("CheckShootSpe");
    }


    protected override void SetData()
    {
        base.SetData();
    }

    //Mouvement
    protected override void Aggro()
    {

        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            isPatroling = false;
            targetPoint = target;
            rb.velocity = Vector2.zero;
            isShooting = true;
        }
        else
        {
            isShooting = false;

        }
    }


    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }



    //Health



    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }




    //Attack



    [SerializeField] protected GameObject EggsProjectiles;
    private bool isSpeRdy = false;
    private bool isSpeReloaded = true;
 

    protected override IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot && !isSpeRdy)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
        else if (isSpeRdy && isShooting && isReadytoShoot)
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
