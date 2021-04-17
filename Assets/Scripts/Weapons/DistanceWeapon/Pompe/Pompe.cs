using System.Collections;
using UnityEngine;

public class Pompe : DistanceWeapon
{

    private int numberOfProj = 3;
    [SerializeField] int angleTir = 0;
    private PompeProjectiles PompeProjectile;
    protected override void GetReferences()
    {
        base.GetReferences();
        PompeProjectile = projectile.GetComponent<PompeProjectiles>();
    }

    protected override IEnumerator Shoot()
    {
        float decalage = angleTir / (numberOfProj - 1);

        PompeProjectile.angleDecalage = -decalage * (numberOfProj + 1) / 2;

        //base.Shoot();
        for (int i = 0; i < numberOfProj; i++)
        {
            PompeProjectile.angleDecalage = PompeProjectile.angleDecalage + decalage;
            Instantiate(projectile, attackPoint.position, transform.rotation);

            BulletInMag--;
            if (BulletInMag <= 0)
            {
                StartCoroutine(Reload());
            }
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;

    }
}
