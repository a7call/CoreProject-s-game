using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class DistanceLaserSniper : DistanceLaser
{
    float timeLocking = 3;
    private bool isLockingEnemy;
    //protected override void Shoot()
    //{
    //    if (isLockingEnemy)
    //    {
    //        dir = (targetSetter.target.position - transform.position);
    //        Debug.DrawRay(transform.position, dir, Color.blue);
    //    }
    //    if (isShootingLasers)
    //    {
    //        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity);
    //        Debug.DrawRay(transform.position, dir, Color.red);
    //        foreach (RaycastHit2D hit in hits)
    //        {
    //            if (hit.transform.gameObject.CompareTag("Player"))
    //            {
    //                PlayerHealth player = hit.transform.gameObject.GetComponent<PlayerHealth>();
    //                player.TakeDamage(damage);
    //            }
    //        }
    //    }
    //}


    //protected override IEnumerator ShootCO()
    //{
    //    if (!isShootingLasers)
    //    {
    //        isLockingEnemy = true;
    //        yield return new WaitForSeconds(timeLocking);
    //        isLockingEnemy = false;
    //        yield return new WaitForSeconds(timeBeforeShoot);
    //        isShootingLasers = true;
    //        yield return new WaitForSeconds(durationOfShoot);
    //        isShootingLasers = false;
    //    }
    //}

    //protected override IEnumerator CanShoot()
    //{
    //    if (isShooting && isreadyToAttack && !isPerturbateurIEM)
    //    {   
    //        isreadyToAttack = false;
    //        StartCoroutine(ShootCO());
    //        yield return new WaitForSeconds(restTime);
    //        isreadyToAttack = true;  
    //    }
    //}

  
}
