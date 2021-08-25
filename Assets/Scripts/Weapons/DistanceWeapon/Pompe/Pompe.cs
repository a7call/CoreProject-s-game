using System.Collections;
using UnityEngine;

public class Pompe : ShootableWeapon
{

    private int numberOfProj = 5;


    protected override IEnumerator Shooting()
    {
        BulletInMag--;
        for (int i = 0; i < numberOfProj; i++)
        {
            float Dispersion = Random.Range(-dispersion, dispersion);
            float projectileSpeed = Random.Range(ProjectileSpeed - 2, ProjectileSpeed);
            ProjectileSetUp(Dispersion, damage, projectileSpeed, enemyLayer,0.5f);           
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;

    }
}
