using System.Collections;
using UnityEngine;

public class BaseShootableWeapon : ShootableWeapon
{
    protected override void GetReferences()
    {
        base.GetReferences();
        PoolManager.GetInstance().CreatePool(projectile, 20);
    }
    protected override IEnumerator Shooting()
    {
        BulletInMag--;
        float Dispersion = Random.Range(-dispersion, dispersion);
        ProjectileSetUp(projectile, Dispersion, damage, ProjectileSpeed, enemyLayer);
        yield return new WaitForSeconds(player.attackSpeed.Value);
        isAttacking = false;
    }

    protected override IEnumerator SpecialShooting()
    {
        StartCoroutine(Shooting());
        Debug.LogWarning("Not implemented");
        yield return null;
    }
}
