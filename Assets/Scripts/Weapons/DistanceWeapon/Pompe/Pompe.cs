using System.Collections;
using UnityEngine;

public class Pompe : ShootableWeapon
{

    private int numberOfProj = 8;
    [SerializeField] private GameObject interruptionProjectile;

    protected override void GetReferences()
    {
        base.GetReferences();
        PoolManager.GetInstance().CreatePool(projectile, 20);
        PoolManager.GetInstance().CreatePool(interruptionProjectile, 5);
        PoolManager.GetInstance().CreatePool(interruptionProjectile.GetComponent<InterruptionProjectile>().explosionEffect, 1);
    }
    protected override IEnumerator Shooting()
    {
        BulletInMag--;
        for (int i = 0; i < numberOfProj; i++)
        {
            float Dispersion = Random.Range(-dispersion, dispersion);
            float projectileSpeed = Random.Range(ProjectileSpeed - ProjectileSpeed/3, ProjectileSpeed);
            ProjectileSetUp(projectile ,Dispersion, damage, projectileSpeed, enemyLayer, 0.5f);           
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;

    }

    protected override IEnumerator SpecialShooting()
    {
        isSpecialReady = false;
        ProjectileSetUp(interruptionProjectile, 0, 0, ProjectileSpeed, enemyLayer, 0.4f);       
        yield return new WaitForSeconds(SpecialAttackDelay);
        isSpecialReady = true;       
    }
}
